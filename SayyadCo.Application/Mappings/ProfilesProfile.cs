using AutoMapper;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Profiles.Commands.UpdateProfileInfo;
using SayyadCo.Application.Features.Profiles.Queries.GetProfileInfo;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class ProfilesProfile : Profile
    {
        public ProfilesProfile()
        {
            CreateMap<AuthUserModel, GetProfileInfoResponseDto>();
            CreateMap<UpdateProfileInfoCommand, UpdateProfileInfoResponseDto>();
            CreateMap<TeacherGame, MyGameResponseDto>()
                .ForMember(dest => dest.SectionTitleAr, opt => opt.MapFrom(src => src.SectionGame.Section.TitleAr))
                .ForMember(dest => dest.SectionTitleEn, opt => opt.MapFrom(src => src.SectionGame.Section.TitleEn))
                .ForMember(dest => dest.GameTitleAr, opt => opt.MapFrom(src => src.SectionGame.Game.TitleAr))
                .ForMember(dest => dest.GameTitleEn, opt => opt.MapFrom(src => src.SectionGame.Game.TitleEn))
                .ForMember(dest => dest.GameTitleEn, opt => opt.MapFrom(src => src.SectionGame.Game.TitleEn))
                .ForMember(dest => dest.GameImage, opt => opt.MapFrom(src => src.SectionGame.Game.Image));
        }
    }
}
