using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Tests.Commands.DeleteQuestion
{
    public class DeleteQuestionFromTestCommandHandler : IRequestHandler<DeleteQuestionFromTestCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionFromTestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteQuestionFromTestCommand request, CancellationToken cancellationToken)
        {
            var question = await _unitOfWork.Questions.GetByIdAsync(request.QuestionId);
            if (question is null)
                return Result<bool>.NotFound("Question not found");

            if (request.TestId != question.TestId)
                return Result<bool>.Failure("This question does not belong to this test");

            _unitOfWork.Questions.Remove(question);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
