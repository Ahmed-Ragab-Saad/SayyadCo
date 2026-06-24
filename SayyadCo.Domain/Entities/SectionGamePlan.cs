using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class SectionGamePlan : BaseEntity
    {
        public string PlanId { get; set; } = string.Empty;
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        //public PlanType PlanType { get; set; }
        public string GameRoleId { get; set; } = string.Empty;

        public Plan Plan { get; set; } = null!;
        public SectionGame SectionGame { get; set; } = null!;
        public GameRole GameRole { get; set; } = null!;
        public ICollection<Code> Codes { get; set; } = new List<Code>();
    }
}
