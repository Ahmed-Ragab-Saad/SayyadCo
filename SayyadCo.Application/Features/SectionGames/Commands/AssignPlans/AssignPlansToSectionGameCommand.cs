using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.SectionGames.Commands.AssignPlans
{
    public class AssignPlansToSectionGameCommand : IRequest<Result<List<AssignPlansResponseDto>>>
    {
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GameId { get; set; } = string.Empty;
        public List<AssignPlanDto> Plans { get; set; } = new();
    }
}
