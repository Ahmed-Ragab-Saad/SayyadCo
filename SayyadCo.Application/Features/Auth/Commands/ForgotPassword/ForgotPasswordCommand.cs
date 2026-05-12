using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Auth.Commands.ForgetPassword
{
    public class ForgotPasswordCommand : IRequest<Result<ForgotPasswordResponseDto>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
