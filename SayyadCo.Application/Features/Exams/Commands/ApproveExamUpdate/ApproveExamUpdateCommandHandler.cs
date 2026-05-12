using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;
using System.Text.Json;

namespace SayyadCo.Application.Features.Exams.Commands.ApproveExamUpdate
{
    public class ApproveExamUpdateCommandHandler : IRequestHandler<ApproveExamUpdateCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApproveExamUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ApproveExamUpdateCommand request, CancellationToken cancellationToken)
        {
            var updateRequest = await _unitOfWork.ExamUpdateRequests.GetByIdAsync(request.RequestId);
            if (updateRequest is null)
                return Result<bool>.NotFound("Update request not found");

            if (updateRequest.Status != RequestStatus.Pending)
                return Result<bool>.Failure("This request has already been processed");

            var exam = await _unitOfWork.Exams.GetByIdAsync(updateRequest.ExamId);
            if (exam is null)
                return Result<bool>.NotFound("Exam not found");

            exam.TitleAr = updateRequest.TitleAr;
            exam.TitleEn = updateRequest.TitleEn;
            exam.DescriptionAr = updateRequest.DescriptionAr;
            exam.DescriptionEn = updateRequest.DescriptionEn;

            var questions = JsonSerializer.Deserialize<List<UpdateQuestionDto>>(updateRequest.QuestionsJson)!;
            var requestIds = questions.Where(q => !string.IsNullOrEmpty(q.Id)).Select(q => q.Id!).ToHashSet();
            var toDelete = exam.Questions.Where(q => !requestIds.Contains(q.Id)).ToList();

            foreach (var q in toDelete)
                _unitOfWork.Questions.Remove(q);

            var existingDict = exam.Questions.ToDictionary(q => q.Id);

            foreach (var questionDto in questions)
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

            updateRequest.Status = RequestStatus.Accepted;
            _unitOfWork.ExamUpdateRequests.Update(updateRequest);

            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
