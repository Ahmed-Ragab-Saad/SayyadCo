using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.Tests.Queries.GetAllTests
{
    public class GetAllTestsResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string? AcademicYearId { get; set; }
        public Semester? Semester { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;
        public int QuestionsCount { get; set; }
    }
}