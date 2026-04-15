using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Entities
{
    public class Section : BaseEntity
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public SectionType SectionType { get; set; }

        //Navigations
        public ICollection<SectionGame> SectionGames { get; set; } = new List<SectionGame>();
    }
}
