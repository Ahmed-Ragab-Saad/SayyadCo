using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public bool IsPrivate { get; set; } = false;
        public string? Password { get; set; }

        public string CreatedByUserId { get; set; } = string.Empty;
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string AcademicYearId { get; set; } = string.Empty;
        public Semester Semester { get; set; }

        //Navigation
        public SectionGame SectionGame { get; set; } = null!;
        public SectionGameAcademicYear SectionGameAcademicYear { get; set; } = null!;
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
    }
}
