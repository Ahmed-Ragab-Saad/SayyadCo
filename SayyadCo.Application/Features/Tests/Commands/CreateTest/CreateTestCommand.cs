using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Enums;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommand : IRequest<Result<CreateTestResponseDto>>
    {
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GameId { get; set; } = string.Empty;
        [JsonIgnore]
        public string AcademicYearId { get; set; } = string.Empty;
        [JsonIgnore]
        public Semester Semester { get; set; }
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;

        public List<QuestionDto> Questions { get; set; } = new();
    }
}
