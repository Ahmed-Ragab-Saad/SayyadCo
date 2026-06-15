namespace SayyadCo.Application.Common.DTOs
{
    public class MyGameResponseDto
    {
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string SectionTitleEn { get; set; } = string.Empty;
        public string SectionTitleAr { get; set; } = string.Empty;
        public string GameTitleEn { get; set; } = string.Empty;
        public string GameTitleAr { get; set; } = string.Empty;
        public string GameImage { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}