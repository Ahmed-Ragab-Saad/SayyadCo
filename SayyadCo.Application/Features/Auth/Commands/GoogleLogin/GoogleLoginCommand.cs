using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Auth.Commands.GoogleLogin
{
    public class GoogleLoginCommand : IRequest<Result<GoogleLoginResponseDto>>
    {
        public string IdToken { get; set; } = string.Empty;
    }
}
