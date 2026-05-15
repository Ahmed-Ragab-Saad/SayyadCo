using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;
using System.Text.Json;

namespace SayyadCo.Application.Features.Exams.Commands.UpdateExam
{
    public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, Result<UpdateExamResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameAccessService _gameAccessService;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateExamCommandHandler(IUnitOfWork unitOfWork, IGameAccessService gameAccessService, IAuthService authService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _gameAccessService = gameAccessService;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<UpdateExamResponseDto>> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<UpdateExamResponseDto>.Unauthorized("Unauthorized");

            var exam = await _unitOfWork.Exams.GetByIdAsync(request.ExamId);
            if (exam is null)
                return Result<UpdateExamResponseDto>.NotFound("Exam not found");

            if (exam.SectionId != request.SectionId ||
                exam.GameId != request.GameId ||
                exam.AcademicYearId != request.AcademicYearId ||
                exam.Semester != request.Semester ||
                exam.GroupId != request.GroupId)
                return Result<UpdateExamResponseDto>.NotFound("Exam not found in this group, academic year, or semester");

            var access = await _gameAccessService.CheckAccessAsync(userId, request.SectionId, request.GameId);
            if (!access.HasAccess)
                return Result<UpdateExamResponseDto>.Forbidden("You are not subscribed to this game");

            if (!access.IsAdminOrSuperAdmin && exam.CreatedByUserId != userId)
                return Result<UpdateExamResponseDto>.Forbidden("You can only update your own exam");

            //if (access.IsAdminOrSuperAdmin)
            //{
            //    await ApplyUpdateDirectly(exam, request);
            //    return Result<UpdateExamResponseDto>.Success(new UpdateExamResponseDto
            //    {
            //        ExamId = exam.Id,
            //        Status = RequestStatus.Accepted,
            //        Message = "Exam updated successfully"
            //    });
            //}

            var existingRequest = await _unitOfWork.ExamUpdateRequests.GetPendingByExamIdAsync(request.ExamId);
            if (existingRequest is not null)
                return Result<UpdateExamResponseDto>.Failure("You already have a pending update request for this exam");

            var updateRequest = new ExamUpdateRequest
            {
                ExamId = request.ExamId,
                RequestedByUserId = userId,
                TitleAr = request.TitleAr,
                TitleEn = request.TitleEn,
                DescriptionAr = request.DescriptionAr,
                DescriptionEn = request.DescriptionEn,
                QuestionsJson = JsonSerializer.Serialize(request.Questions),
                Status = RequestStatus.Pending
            };

            await _unitOfWork.ExamUpdateRequests.AddAsync(updateRequest);
            await _unitOfWork.SaveChangesAsync();

            return Result<UpdateExamResponseDto>.Success(new UpdateExamResponseDto
            {
                ExamId = exam.Id,
                Status = RequestStatus.Pending,
                Message = "Update request submitted and waiting for admin approval"
            });
        }

        private async Task ApplyUpdateDirectly(Exam exam, UpdateExamCommand request)
        {
            exam.TitleAr = request.TitleAr;
            exam.TitleEn = request.TitleEn;
            exam.DescriptionAr = request.DescriptionAr;
            exam.DescriptionEn = request.DescriptionEn;

            var requestIds = request.Questions
                .Where(q => !string.IsNullOrEmpty(q.Id))
                .Select(q => q.Id!)
                .ToHashSet();

            var toDelete = exam.Questions
                .Where(q => !requestIds.Contains(q.Id))
                .ToList();

            foreach (var q in toDelete)
                _unitOfWork.Questions.Remove(q);

            var existingDict = exam.Questions.ToDictionary(q => q.Id);

            foreach (var questionDto in request.Questions)
            {
                if (!string.IsNullOrEmpty(questionDto.Id) && existingDict.TryGetValue(questionDto.Id, out var existing))
                {
                    existing.ContentJson = questionDto.ContentJson;
                    existing.Points = questionDto.Points;
                    _unitOfWork.Questions.Update(existing);
                }
                else
                {
                    await _unitOfWork.Questions.AddAsync(new Question
                    {
                        ExamId = exam.Id,
                        ContentJson = questionDto.ContentJson,
                        Points = questionDto.Points
                    });
                }
            }

            _unitOfWork.Exams.Update(exam);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
