using AutoMapper;
using SayyadCo.Application.Features.SectionGames.Queries.GetSectionGamePlans;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class SectionGamePlanProfile : Profile
    {
        public SectionGamePlanProfile()
        {
            CreateMap<SectionGamePlan, GetSectionGamePlansResponseDto>()
                .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.PlanId))
                .ForMember(dest => dest.PlanTitleEn, opt => opt.MapFrom(src => src.Plan.TitleEn))
                .ForMember(dest => dest.PlanTitleAr, opt => opt.MapFrom(src => src.Plan.TitleAr))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Plan.Price))
                .ForMember(dest => dest.DurationInDays, opt => opt.MapFrom(src => src.Plan.DurationInDays));
        }
    }
}
