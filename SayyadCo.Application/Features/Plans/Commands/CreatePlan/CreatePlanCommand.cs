using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Plans.Commands.CreatePlan
{
    public class CreatePlanCommand : IRequest<Result<CreatePlanResponseDto>>
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
        public decimal Price { get; set; }
    }
}
