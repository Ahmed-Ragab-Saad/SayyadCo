using FluentValidation;

namespace SayyadCo.Application.Features.AcademicYears.Commands.UpdateAcademicYear
{
    public class UpdateAcademicYearValidator : AbstractValidator<UpdateAcademicYearCommand>
    {
        public UpdateAcademicYearValidator()
        {
            RuleFor(x => x.TitleAr)
                .MinimumLength(3);

            RuleFor(x => x.TitleEn)
                .MinimumLength(3);
        }
    }
}
