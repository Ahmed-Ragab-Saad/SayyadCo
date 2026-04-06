using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Entities
{
    public class Exam : BaseAssessment
    {
        public ExamStatus Status { get; set; } = ExamStatus.Pending;
    }
}
