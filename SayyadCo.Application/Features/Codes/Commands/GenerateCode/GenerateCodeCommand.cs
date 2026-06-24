using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Codes.Commands.GenerateCode
{
    public class GenerateCodeCommand : IRequest<Result<GenerateCodeResponseDto>>
    {
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GameId { get; set; } = string.Empty;
        public string SectionGamePlanId { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
