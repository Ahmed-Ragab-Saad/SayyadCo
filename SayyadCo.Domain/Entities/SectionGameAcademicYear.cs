using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class SectionGameAcademicYear : Entity
    {
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string AcademicYearId { get; set; } = string.Empty;

        public SectionGame SectionGame { get; set; } = null!;
        public AcademicYear AcademicYear { get; set; } = null!;
    }
}
