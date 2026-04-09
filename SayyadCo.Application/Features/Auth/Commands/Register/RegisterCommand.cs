using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<Result<RegisterResponseDto>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
