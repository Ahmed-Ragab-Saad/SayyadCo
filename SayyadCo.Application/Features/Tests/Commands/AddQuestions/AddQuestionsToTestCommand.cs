using MediatR;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Tests.Commands.AddQuestions
{
    public class AddQuestionsToTestCommand : IRequest<Result<List<AddQuestionResponseDto>>>
    {
        [JsonIgnore]
        public string TestId { get; set; } = string.Empty;
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
