using FluentValidation;

namespace SayyadCo.Application.Features.Tests.Commands.AddQuestions
{
    public class AddQuestionsToTestCommandValidator : AbstractValidator<AddQuestionsToTestCommand>
    {
        public AddQuestionsToTestCommandValidator()
        {
            RuleFor(x => x.TestId).NotEmpty();

            RuleFor(x => x.Questions)
                .NotEmpty()
                .WithMessage("At least one question is required");

            RuleForEach(x => x.Questions).ChildRules(q =>
            {
                q.RuleFor(x => x.ContentJson).NotEmpty();
                q.RuleFor(x => x.Points).GreaterThan(0);
            });
        }
    }
}
