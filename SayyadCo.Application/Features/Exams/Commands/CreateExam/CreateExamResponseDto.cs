using SayyadCo.Application.Features.Tests.Commands.AddQuestions;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string AcademicYearId { get; set; } = string.Empty;
        public Semester Semester { get; set; }
        public ExamStatus Status { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;
        public List<AddQuestionResponseDto> Questions { get; set; } = new();
    }
}