using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Features.Tests.Commands.AddQuestions;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, Result<CreateExamResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGameAccessService _gameAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateExamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IGameAccessService gameAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gameAccessService = gameAccessService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<CreateExamResponseDto>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<CreateExamResponseDto>.Unauthorized("Unauthorized");

            var access = await _gameAccessService.CheckAccessAsync(userId, request.SectionId, request.GameId);
            if (!access.HasAccess)
                return Result<CreateExamResponseDto>.Forbidden("You are not subscribed to this game");

            if (!access.IsTeacher && !access.IsAdminOrSuperAdmin)
                return Result<CreateExamResponseDto>.Forbidden("Only Teachers and Admins can create exams");

            var group = await _unitOfWork.Groups.GetByIdAsync(request.GroupId);
            if (group is null)
                return Result<CreateExamResponseDto>.NotFound("Group not found");

            if (group.SectionId != request.SectionId || group.GameId != request.GameId)
                return Result<CreateExamResponseDto>.Failure("Group does not belong to this SectionGame");

            if (access.IsTeacher && group.CreatedByUserId != userId)
                return Result<CreateExamResponseDto>.Forbidden("You can only create exams in your own group");

            var academicYear = await _unitOfWork.AcademicYears.GetByIdAsync(request.AcademicYearId);
            if (academicYear is null)
                return Result<CreateExamResponseDto>.NotFound("Academic year not found");

            var exam = _mapper.Map<Exam>(request);
            exam.Status = access.IsAdminOrSuperAdmin ? ExamStatus.Approved : ExamStatus.Pending;

            await _unitOfWork.Exams.AddAsync(exam);

            var questions = request.Questions.Select(q => new Question
            {
                ExamId = exam.Id,
                ContentJson = q.ContentJson,
                Points = q.Points
            }).ToList();

            await _unitOfWork.Questions.AddQuestions(questions);

            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<CreateExamResponseDto>(exam);
            response.Questions = _mapper.Map<List<AddQuestionResponseDto>>(questions);

            return Result<CreateExamResponseDto>.Success(response);
        }
    }
}
