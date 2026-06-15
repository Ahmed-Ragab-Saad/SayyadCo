using AutoMapper;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Profiles.Commands.UpdateProfileInfo;
using SayyadCo.Application.Features.Profiles.Queries.GetProfileInfo;

namespace SayyadCo.Application.Mappings
{
    public class ProfilesProfile : Profile
    {
        public ProfilesProfile()
        {
            CreateMap<AuthUserModel, GetProfileInfoResponseDto>();
            CreateMap<UpdateProfileInfoCommand, UpdateProfileInfoResponseDto>();
        }
    }
}
