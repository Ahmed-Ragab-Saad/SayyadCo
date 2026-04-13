namespace SayyadCo.Application.Features.SectionGames.Queries.GetSectionGames
{
    public class GetSectionGamesResponseDto
    {
        public string GameId { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}