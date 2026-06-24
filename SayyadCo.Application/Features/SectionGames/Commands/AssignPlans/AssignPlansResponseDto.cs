using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.SectionGames.Commands.AssignPlans
{
    public class AssignPlansResponseDto
    {
        public string PlanId { get; set; } = string.Empty;
        public string PlanTitleEn { get; set; } = string.Empty;
        public string PlanTitleAr { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public PlanType PlanType { get; set; }
        public string GameRole { get; set; } = string.Empty;
    }
}