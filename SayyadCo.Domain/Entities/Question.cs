using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class Question : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string Option1 { get; set; } = string.Empty;
        public string Option2 { get; set; } = string.Empty;
        public string Option3 { get; set; } = string.Empty;
        public string Option4 { get; set; } = string.Empty;
        public int CorrectAnswer { get; set; }

        // Foreign Keys
        public string? ExamId { get; set; }
        public string? TestId { get; set; }

        // Navigations
        public Exam? Exam { get; set; }
        public Test? Test { get; set; }
    }
}
