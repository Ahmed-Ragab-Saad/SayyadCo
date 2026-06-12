using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Auth.Commands.FacebookLogin
{
    public class FacebookLoginCommand : IRequest<Result<FacebookLoginResponseDto>>
    {
        public string Code { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
    }
}
