using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Entities
{
    public class SectionGamePlan : BaseEntity
    {
        public string PlanId { get; set; } = string.Empty;
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public PlanType PlanType { get; set; }

        public Plan Plan { get; set; } = null!;
        public SectionGame SectionGame { get; set; } = null!;
    }
}
