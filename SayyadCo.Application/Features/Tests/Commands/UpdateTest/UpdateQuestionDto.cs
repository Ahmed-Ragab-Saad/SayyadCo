namespace SayyadCo.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateQuestionDto
    {
        public string? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string ContentJson { get; set; } = string.Empty;
        public int Points { get; set; } = 1;
    }
}
