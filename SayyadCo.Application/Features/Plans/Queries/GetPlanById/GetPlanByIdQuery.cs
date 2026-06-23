using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Plans.Queries.GetPlanById
{
    public class GetPlanByIdQuery : IRequest<Result<GetPlanByIdResponseDto>>
    {
        [JsonIgnore]
        public string Id { get; set; } = string.Empty;
    }
}
