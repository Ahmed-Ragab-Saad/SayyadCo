using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class Question : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string ContentJson { get; set; } = string.Empty;

        // Foreign Keys
        public string? ExamId { get; set; }
        public string? TestId { get; set; }

        // Navigations
        public Exam? Exam { get; set; }
        public Test? Test { get; set; }
    }
}
