using SayyadCo.Application.Common.DTOs;

namespace SayyadCo.Application.Features.Tests.Queries.GetTestById
{
    public class GetTestByIdResponseDto
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
