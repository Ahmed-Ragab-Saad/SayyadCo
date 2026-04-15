namespace SayyadCo.Application.Common.DTOs
{
    public class AddGameToSectionResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public List<string> AddedGames { get; set; } = new();
        public List<string> FailedGames { get; set; } = new();
        //public string SectionId { get; set; } = string.Empty;
        //public string GameId { get; set; } = string.Empty;
        //public string SectionTitleEn { get; set; } = string.Empty;
        //public string GameTitleEn { get; set; } = string.Empty;
    }
}