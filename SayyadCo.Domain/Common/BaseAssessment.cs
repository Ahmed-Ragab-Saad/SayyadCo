using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Common
{
    public abstract class BaseAssessment : BaseEntity
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string? AcademicYearId { get; set; }
        public Semester? Semester { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;

        // Navigations
        public Group Group { get; set; } = null!;
        public AcademicYear? AcademicYear { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
