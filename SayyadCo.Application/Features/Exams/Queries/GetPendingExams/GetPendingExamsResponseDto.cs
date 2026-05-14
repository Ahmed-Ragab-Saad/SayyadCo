namespace SayyadCo.Application.Features.Exams.Queries.GetPendingExams
{
    public class GetPendingExamsResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string CreatedByUserId { get; set; } = string.Empty;
        public int QuestionsCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}