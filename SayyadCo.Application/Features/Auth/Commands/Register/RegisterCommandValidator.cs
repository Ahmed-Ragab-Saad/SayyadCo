using FluentValidation;

namespace SayyadCo.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Must(x => x.Trim().Length >= 3)
                .WithMessage("First name must be at least 3 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Must(x => x.Trim().Length >= 3)
                .WithMessage("Last name must be at least 3 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid Email");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }
}
