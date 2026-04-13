using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string GameTypeId { get; set; } = string.Empty;

        //Navigations
        public GameType GameType { get; set; } = null!;
        public ICollection<SectionGame> SectionGames { get; set; } = new List<SectionGame>();
    }
}
