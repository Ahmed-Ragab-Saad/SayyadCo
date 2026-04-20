using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Tests.Commands.AcceptTest
{
    public class AcceptTestCommand : IRequest<Result<bool>>
    {
        public string TestId { get; set; } = string.Empty;
    }
}
