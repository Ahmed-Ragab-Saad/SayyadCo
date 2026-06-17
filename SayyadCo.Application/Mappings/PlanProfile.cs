using AutoMapper;
using SayyadCo.Application.Features.Plans.Commands.CreatePlan;
using SayyadCo.Application.Features.Plans.Commands.UpdatePlan;
using SayyadCo.Application.Features.Plans.Queries.GetAllPlans;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class PlanMappingProfile : Profile
    {
        public PlanMappingProfile()
        {
            CreateMap<CreatePlanCommand, Plan>();
            CreateMap<Plan, CreatePlanResponseDto>();
            CreateMap<UpdatePlanCommand, Plan>();
            CreateMap<Plan, UpdatePlanResponseDto>();
            CreateMap<Plan, GetAllPlansResponseDto>();
        }
    }
}
