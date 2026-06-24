using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class Code : BaseEntity
    {
        public string Value { get; set; } = string.Empty;
        public bool IsUsed { get; set; } = false;
        public string? UsedByUserId { get; set; }
        public DateTime? UsedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string GameRoleId { get; set; } = string.Empty;
        public string SectionGamePlanId { get; set; } = string.Empty;

        //Navigations
        public SectionGame SectionGame { get; set; } = null!;
        public GameRole GameRole { get; set; } = null!;
        public SectionGamePlan SectionGamePlan { get; set; } = null!;
    }
}
