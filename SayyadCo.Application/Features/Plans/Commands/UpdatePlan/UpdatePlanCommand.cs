using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Plans.Commands.UpdatePlan
{
    public class UpdatePlanCommand : IRequest<Result<UpdatePlanResponseDto>>
    {
        [JsonIgnore]
        public string Id { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
        public decimal Price { get; set; }
    }
}
