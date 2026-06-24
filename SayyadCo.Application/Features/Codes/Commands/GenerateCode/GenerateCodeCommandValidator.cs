using FluentValidation;

namespace SayyadCo.Application.Features.Codes.Commands.GenerateCode
{
    public class GenerateCodeCommandValidator : AbstractValidator<GenerateCodeCommand>
    {
        public GenerateCodeCommandValidator()
        {
            RuleFor(x => x.SectionGamePlanId).NotEmpty();
            RuleFor(x => x.ExpiresAt)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Expiry date must be in the future");
        }
    }
}
