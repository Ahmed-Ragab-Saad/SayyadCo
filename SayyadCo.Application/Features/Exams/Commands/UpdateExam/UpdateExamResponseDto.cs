using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.Exams.Commands.UpdateExam
{
    public class UpdateExamResponseDto
    {
        public string ExamId { get; set; } = string.Empty;
        public RequestStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}