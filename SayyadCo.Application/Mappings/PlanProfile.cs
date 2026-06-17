using AutoMapper;
using SayyadCo.Application.Features.Plans.Commands.CreatePlan;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class PlanMappingProfile : Profile
    {
        public PlanMappingProfile()
        {
            CreateMap<CreatePlanCommand, Plan>();
            CreateMap<Plan, CreatePlanResponseDto>();
        }
    }
}
