namespace SayyadCo.Application.Features.Exams.Commands.CreateExam
{
    public class ExamQuestionsResponse
    {
        public string Id { get; set; } = string.Empty;
        public string ExamId { get; set; } = string.Empty;
        public string ContentJson { get; set; } = string.Empty;
        public int Points { get; set; }
    }
}