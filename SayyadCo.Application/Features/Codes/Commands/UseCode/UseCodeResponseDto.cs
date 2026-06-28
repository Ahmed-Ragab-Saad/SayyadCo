namespace SayyadCo.Application.Features.Codes.Commands.UseCode
{
    public class UseCodeResponseDto
    {
        public string SectionId { get; set; } = string.Empty;
        public string SectionTitleEn { get; set; } = string.Empty;
        public string SectionTitleAr { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string GameTitleEn { get; set; } = string.Empty;
        public string GameTitleAr { get; set; } = string.Empty;
        public string GameRole { get; set; } = string.Empty;
        public string PlanTitleEn { get; set; } = string.Empty;
        public string PlanTitleAr { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}