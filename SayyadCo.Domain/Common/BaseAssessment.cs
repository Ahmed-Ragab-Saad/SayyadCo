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
        public string? AcademicYearId { get; set; }
        public Semester? Semester { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;

        // Navigations
        public SectionGame SectionGame { get; set; } = null!;
        public AcademicYear? AcademicYear { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
