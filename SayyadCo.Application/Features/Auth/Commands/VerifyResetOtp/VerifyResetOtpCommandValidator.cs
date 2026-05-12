using FluentValidation;

namespace SayyadCo.Application.Features.Auth.Commands.VerifyResetOtp
{
    public class VerifyResetOtpCommandValidator : AbstractValidator<VerifyResetOtpCommand>
    {
        public VerifyResetOtpCommandValidator()
        {
            RuleFor(x => x.ResetToken).NotEmpty();
            RuleFor(x => x.OtpCode)
                .NotEmpty()
                .Length(6)
                .Matches(@"^\d{6}$")
                .WithMessage("OTP must be 6 digits");
        }
    }
}
