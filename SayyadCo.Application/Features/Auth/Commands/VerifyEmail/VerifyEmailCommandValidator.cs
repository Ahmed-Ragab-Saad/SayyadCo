using FluentValidation;

namespace SayyadCo.Application.Features.Auth.Commands.VerifyEmail
{
    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.OtpCode)
                .NotEmpty()
                .Length(6)
                .Matches(@"^\d{6}$")
                .WithMessage("OTP must be 6 digits");
        }
    }
}
