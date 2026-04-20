namespace SayyadCo.Application.Features.Tests.Commands.AddQuestions
{
    public class AddQuestionResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string TestId { get; set; } = string.Empty;
        public string ContentJson { get; set; } = string.Empty;
        public int Points { get; set; }
    }
}