using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Profiles.Commands.UpdateProfileInfo
{
    public class UpdateProfileInfoCommand : IRequest<Result<UpdateProfileInfoResponseDto>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
