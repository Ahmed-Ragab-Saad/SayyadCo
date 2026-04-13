using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty; //Default if null
        public bool IsPrivate { get; set; } = false;
        public string? Password { get; set; }

        public string CreatedByUserId { get; set; } = string.Empty;
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;

        //Navigation
        public SectionGame SectionGame { get; set; } = null!;
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Test> Tests { get; set; } = new List<Test>();
        public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
    }
}
