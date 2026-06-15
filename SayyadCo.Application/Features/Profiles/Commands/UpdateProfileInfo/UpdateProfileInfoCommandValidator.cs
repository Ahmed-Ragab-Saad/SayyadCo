using FluentValidation;

namespace SayyadCo.Application.Features.Profiles.Commands.UpdateProfileInfo
{
    public class UpdateProfileInfoCommandValidator : AbstractValidator<UpdateProfileInfoCommand>
    {
        public UpdateProfileInfoCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Image)
                .Must(x => string.IsNullOrEmpty(x) || Uri.TryCreate(x, UriKind.Absolute, out _))
                .WithMessage("Image must be a valid URL");
        }
    }
}
