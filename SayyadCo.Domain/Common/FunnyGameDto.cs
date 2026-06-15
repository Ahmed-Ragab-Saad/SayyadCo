namespace SayyadCo.Domain.Common
{
    public class FunnyGameDto
    {
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string SectionTitleEn { get; set; } = string.Empty;
        public string SectionTitleAr { get; set; } = string.Empty;
        public string GameTitleEn { get; set; } = string.Empty;
        public string GameTitleAr { get; set; } = string.Empty;
        public string GameImage { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
    }
}
