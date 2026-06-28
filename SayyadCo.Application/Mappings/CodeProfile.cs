using AutoMapper;
using SayyadCo.Application.Features.Codes.Commands.UseCode;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class CodeProfile : Profile
    {
        public CodeProfile()
        {
            CreateMap<Code, UseCodeResponseDto>()
                .ForMember(dest => dest.SectionTitleEn, opt => opt.MapFrom(src => src.SectionGame.Section.TitleEn))
                .ForMember(dest => dest.SectionTitleAr, opt => opt.MapFrom(src => src.SectionGame.Section.TitleAr))
                .ForMember(dest => dest.GameTitleEn, opt => opt.MapFrom(src => src.SectionGame.Game.TitleEn))
                .ForMember(dest => dest.GameTitleAr, opt => opt.MapFrom(src => src.SectionGame.Game.TitleAr))
                .ForMember(dest => dest.GameRole, opt => opt.MapFrom(src => src.GameRole.Role))
                .ForMember(dest => dest.PlanTitleEn, opt => opt.MapFrom(src => src.SectionGamePlan.Plan.TitleEn))
                .ForMember(dest => dest.PlanTitleAr, opt => opt.MapFrom(src => src.SectionGamePlan.Plan.TitleAr))
                .ForMember(dest => dest.StartDate, opt => opt.Ignore())
                .ForMember(dest => dest.ExpirationDate, opt => opt.Ignore());
        }
    }
}
