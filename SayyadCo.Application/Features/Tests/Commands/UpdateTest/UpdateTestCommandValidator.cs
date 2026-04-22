using FluentValidation;

namespace SayyadCo.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommandValidator : AbstractValidator<UpdateTestCommand>
    {
        public UpdateTestCommandValidator()
        {
            RuleFor(x => x.TitleAr).NotEmpty().MaximumLength(200);
            RuleFor(x => x.TitleEn).NotEmpty().MaximumLength(200);
            RuleFor(x => x.DescriptionAr).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.DescriptionEn).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.SectionId).NotEmpty();
            RuleFor(x => x.GameId).NotEmpty();
            RuleFor(x => x.AcademicYearId).NotEmpty();
            RuleFor(x => x.Semester).IsInEnum();
            RuleForEach(x => x.Questions).ChildRules(q =>
            {
                q.RuleFor(x => x.Title).NotEmpty();
                q.RuleFor(x => x.ContentJson).NotEmpty();
                q.RuleFor(x => x.Points).GreaterThan(0);
            });
        }
    }
}
