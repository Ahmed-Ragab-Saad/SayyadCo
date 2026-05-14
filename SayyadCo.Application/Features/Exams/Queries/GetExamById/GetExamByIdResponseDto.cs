using SayyadCo.Application.Common.DTOs;

namespace SayyadCo.Application.Features.Exams.Queries.GetExamById
{
    public class GetExamByIdResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string? RejectionReason { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;
        public List<QuestionDto> Questions { get; set; } = new();
    }
}