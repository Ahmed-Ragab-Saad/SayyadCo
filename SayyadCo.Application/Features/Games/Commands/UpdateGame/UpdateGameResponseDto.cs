namespace SayyadCo.Application.Features.Games.Commands.UpdateGame
{
    public class UpdateGameResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        //public string GameTypeId { get; set; } = string.Empty;
    }
}