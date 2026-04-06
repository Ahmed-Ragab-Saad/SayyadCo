using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class Section : BaseEntity
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        //Navigations
        public ICollection<SectionGame> SectionGames { get; set; } = new List<SectionGame>();
    }
}
