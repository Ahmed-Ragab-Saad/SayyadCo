using MediatR;
using Microsoft.AspNetCore.Http;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;

namespace SayyadCo.Application.Features.Tests.Commands.AddQuestions
{
    public class AddQuestionsToTestCommandHandler : IRequestHandler<AddQuestionsToTestCommand, Result<List<AddQuestionResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddQuestionsToTestCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<List<AddQuestionResponseDto>>> Handle(AddQuestionsToTestCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Result<List<AddQuestionResponseDto>>.Failure("Unauthorized");

            var test = await _unitOfWork.Tests.GetByIdAsync(request.TestId);
            if (test is null)
                return Result<List<AddQuestionResponseDto>>.NotFound("Test not found");

            if (test.CreatedByUserId != userId)
                return Result<List<AddQuestionResponseDto>>.Failure("You are not authorized to add questions to this test");

            var questionsList = request.Questions.Select(q => new Question
            {
                TestId = request.TestId,
                ContentJson = q.ContentJson,
                Points = q.Points
            }).ToList();

            await _unitOfWork.Questions.AddQuestions(questionsList);
            await _unitOfWork.SaveChangesAsync();

            var response = questionsList.Select(q => new AddQuestionResponseDto
            {
                Id = q.Id,
                TestId = q.TestId!,
                ContentJson = q.ContentJson,
                Points = q.Points
            }).ToList();

            return Result<List<AddQuestionResponseDto>>.Success(response);
        }
    }
}
