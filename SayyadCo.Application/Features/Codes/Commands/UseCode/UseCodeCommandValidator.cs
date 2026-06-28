using FluentValidation;

namespace SayyadCo.Application.Features.Codes.Commands.UseCode
{
    public class UseCodeCommandValidator : AbstractValidator<UseCodeCommand>
    {
        public UseCodeCommandValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}
