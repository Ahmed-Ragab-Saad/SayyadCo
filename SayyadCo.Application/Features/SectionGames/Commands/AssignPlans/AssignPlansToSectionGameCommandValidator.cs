using FluentValidation;

namespace SayyadCo.Application.Features.SectionGames.Commands.AssignPlans
{
    public class AssignPlansToSectionGameCommandValidator : AbstractValidator<AssignPlansToSectionGameCommand>
    {
        public AssignPlansToSectionGameCommandValidator()
        {
            RuleFor(x => x.Plans)
                .NotEmpty()
                .WithMessage("At least one plan is required");

            RuleForEach(x => x.Plans).ChildRules(p =>
            {
                p.RuleFor(x => x.PlanId).NotEmpty();
                p.RuleFor(x => x.GameRoleId)
                    .NotEmpty()
                    .WithMessage("Invalid game role");
            });
        }
    }
}
