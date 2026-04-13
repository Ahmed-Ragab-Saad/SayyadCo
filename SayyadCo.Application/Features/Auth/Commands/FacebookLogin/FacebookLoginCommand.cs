using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Auth.Commands.FacebookLogin
{
    public class FacebookLoginCommand : IRequest<Result<FacebookLoginResponseDto>>
    {
        public string AccessToken { get; set; } = string.Empty;
    }
}
