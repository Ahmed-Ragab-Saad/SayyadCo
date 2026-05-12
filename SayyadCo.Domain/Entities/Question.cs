using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Entities
{
    public class Question : BaseEntity
    {
        public string? Image { get; set; }
        public string ContentJson { get; set; } = string.Empty;
        public int Points { get; set; } = 1;
        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        // Foreign Keys
        public string? ExamId { get; set; }
        public string? TestId { get; set; }

        // Navigations
        public Exam? Exam { get; set; }
        public Test? Test { get; set; }
    }
}
