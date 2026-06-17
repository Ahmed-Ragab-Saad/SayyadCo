using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Plans.Commands.DeletePlan
{
    public class DeletePlanCommand : IRequest<Result<bool>>
    {
        [JsonIgnore]
        public string Id { get; set; } = string.Empty;
    }
}
