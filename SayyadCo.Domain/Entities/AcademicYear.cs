using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class AcademicYear : BaseEntity
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;

        //Navigations
        public ICollection<SectionGameAcademicYear> SectionGameAcademicYears { get; set; } = new List<SectionGameAcademicYear>();
        public ICollection<Test> Tests { get; set; } = new List<Test>();
    }
}
