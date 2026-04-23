using FluentValidation;

namespace SayyadCo.Application.Features.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.Image)
                .NotEmpty()
                .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
                .WithMessage("Image must be a valid URL");

            RuleFor(x => x.Password)
                .NotEmpty()
                .When(x => x.IsPrivate)
                .WithMessage("Password is required for private groups");

            RuleFor(x => x.Password)
                .MaximumLength(100)
                .When(x => x.Password != null);
        }
    }
}
