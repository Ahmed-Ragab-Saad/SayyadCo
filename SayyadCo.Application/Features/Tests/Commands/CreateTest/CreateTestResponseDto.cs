using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string? GroupId { get; set; }
        public string? AcademicYearId { get; set; }
        public Semester? Semester { get; set; }
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string CreatedByUserId { get; set; } = string.Empty;
    }
}