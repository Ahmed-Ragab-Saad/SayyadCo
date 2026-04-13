namespace SayyadCo.Application.Features.Games.Queries.GetAllGames
{
    public class GetAllGamesResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}