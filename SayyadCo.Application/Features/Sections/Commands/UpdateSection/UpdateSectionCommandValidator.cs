using FluentValidation;

namespace SayyadCo.Application.Features.Sections.Commands.UpdateSection
{
    public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
    {
        public UpdateSectionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.TitleAr)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.TitleEn)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.DescriptionAr)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.DescriptionEn)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Image)
                .NotEmpty()
                .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
                .WithMessage("Image must be a valid URL");
        }
    }
}
