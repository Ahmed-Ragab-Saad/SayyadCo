using FluentValidation;

namespace SayyadCo.Application.Features.Exams.Commands.UpdateExam
{
    public class UpdateExamCommandValidator : AbstractValidator<UpdateExamCommand>
    {
        public UpdateExamCommandValidator()
        {
            RuleFor(x => x.TitleAr).NotEmpty().MaximumLength(200);
            RuleFor(x => x.TitleEn).NotEmpty().MaximumLength(200);
            RuleFor(x => x.DescriptionAr).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.DescriptionEn).NotEmpty().MaximumLength(1000);

            RuleForEach(x => x.Questions).ChildRules(q =>
            {
                q.RuleFor(x => x.ContentJson).NotEmpty();
                q.RuleFor(x => x.Points).GreaterThan(0);
            });
        }
    }
}
