using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.SectionGames.Queries.GetSectionGamePlans
{
    public class GetSectionGamePlansQuery : IRequest<Result<List<GetSectionGamePlansResponseDto>>>
    {
        [JsonIgnore]
        public string SectionId { get; set; } = string.Empty;
        [JsonIgnore]
        public string GameId { get; set; } = string.Empty;
    }
}
