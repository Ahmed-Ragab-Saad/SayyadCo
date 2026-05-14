namespace SayyadCo.Application.Features.Exams.Queries.GetPendingExamUpdates
{
    public class GetPendingExamUpdatesResponseDto
    {
        public string RequestId { get; set; } = string.Empty;
        public string ExamId { get; set; } = string.Empty;
        public string ExamTitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string RequestedByUserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}