using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.SectionGames.Commands.AssignPlans
{
    public class AssignPlanDto
    {
        public string PlanId { get; set; } = string.Empty;
        public PlanType PlanType { get; set; }
    }
}