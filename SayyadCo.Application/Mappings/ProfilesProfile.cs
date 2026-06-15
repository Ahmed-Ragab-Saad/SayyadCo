using AutoMapper;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Profiles;

namespace SayyadCo.Application.Mappings
{
    public class ProfilesProfile : Profile
    {
        public ProfilesProfile()
        {
            CreateMap<AuthUserModel, GetProfileInfoResponseDto>();
        }
    }
}
