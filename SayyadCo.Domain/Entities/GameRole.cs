using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class GameRole : BaseEntity
    {
        public string Role { get; set; } = string.Empty;

        //Navigation
        public ICollection<Code> Codes { get; set; } = new List<Code>();
        public ICollection<SectionGamePlan> SectionGamePlans { get; set; } = new List<SectionGamePlan>();
    }
}
