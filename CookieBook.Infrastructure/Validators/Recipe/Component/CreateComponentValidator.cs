using CookieBook.Infrastructure.Commands.Recipe.Component;
using FluentValidation;

namespace CookieBook.Infrastructure.Validators.Recipe.Component
{
    public class CreateComponentValidator : AbstractValidator<CreateComponent>
    {
        public CreateComponentValidator()
        {
			RuleFor(x => x.Name)
				.NotEmpty()
				.MinimumLength(2)
				.MaximumLength(255);

			RuleFor(x => x.Unit)
				.NotEmpty()
				.IsInEnum();

			RuleFor(x => x.Amount)
				.NotEmpty()
				.GreaterThan(0.0f);
		}
    }
}