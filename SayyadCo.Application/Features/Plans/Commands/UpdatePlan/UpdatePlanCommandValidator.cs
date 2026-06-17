using FluentValidation;

namespace SayyadCo.Application.Features.Plans.Commands.UpdatePlan
{
    public class UpdatePlanCommandValidator : AbstractValidator<UpdatePlanCommand>
    {
        public UpdatePlanCommandValidator()
        {
            RuleFor(x => x.TitleAr).NotEmpty().MaximumLength(200);
            RuleFor(x => x.TitleEn).NotEmpty().MaximumLength(200);
            RuleFor(x => x.DescriptionAr).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.DescriptionEn).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.DurationInDays).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        }
    }
}
