namespace SayyadCo.Application.Common.DTOs
{
    public class QuestionDto
    {
        public string? Image { get; set; }
        public string ContentJson { get; set; } = string.Empty;
        public int Points { get; set; } = 1;
    }
}
