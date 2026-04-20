using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Tests.Commands.DeleteQuestion
{
    public class DeleteQuestionFromTestCommand : IRequest<Result<bool>>
    {
        public string TestId { get; set; } = string.Empty;
        public string QuestionId { get; set; } = string.Empty;
    }
}
