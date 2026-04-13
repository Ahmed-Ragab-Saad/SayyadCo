namespace SayyadCo.Application.Features.SectionGames.Commands.AddGameToSection
{
    public class AddGameToSectionResponseDto
    {
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string SectionTitleEn { get; set; } = string.Empty;
        public string GameTitleEn { get; set; } = string.Empty;
    }
}