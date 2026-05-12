using FluentValidation;
using SayyadCo.Application.Features.Auth.Commands.ForgetPassword;

namespace SayyadCo.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
