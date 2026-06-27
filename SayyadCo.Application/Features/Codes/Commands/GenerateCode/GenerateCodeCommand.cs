using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Codes.Commands.GenerateCode
{
    public class GenerateCodeCommand : IRequest<Result<GenerateCodeResponseDto>>
    {
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string SectionGamePlanId { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
