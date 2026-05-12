namespace SayyadCo.Application.Common.DTOs
{
    public class UpdateQuestionDto
    {
        public string? Id { get; set; }
        public string? Image { get; set; }
        public string ContentJson { get; set; } = string.Empty;
        public int Points { get; set; } = 1;
    }
}