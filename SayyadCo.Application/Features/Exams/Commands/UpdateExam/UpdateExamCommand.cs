using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Enums;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Exams.Commands.UpdateExam
{
    public class UpdateExamCommand : IRequest<Result<UpdateExamResponseDto>>
    {
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GameId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GroupId { get; set; } = string.Empty;
        [JsonIgnore]
        public string AcademicYearId { get; set; } = string.Empty;
        [JsonIgnore]
        public Semester Semester { get; set; }
        [JsonIgnore]
        public string ExamId { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public List<UpdateQuestionDto> Questions { get; set; } = new();
    }
}
