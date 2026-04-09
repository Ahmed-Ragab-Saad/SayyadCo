using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<Result<LoginResponseDto>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
