using FluentValidation;

namespace SayyadCo.Application.Features.AcademicYears.Commands.AddAcademicYear
{
    public class AddAcademicYearValidator : AbstractValidator<AddAcademicYearCommand>
    {
        public AddAcademicYearValidator()
        {
            RuleFor(x => x.TitleAr)
                .MinimumLength(3);

            RuleFor(x => x.TitleEn)
                .MinimumLength(3);
        }
    }
}
