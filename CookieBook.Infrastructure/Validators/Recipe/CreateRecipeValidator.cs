using CookieBook.Infrastructure.Commands.Recipe;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Validators.Recipe.Component;
using FluentValidation;

namespace CookieBook.Infrastructure.Validators.Recipe
{
    public class CreateRecipeValidator : AbstractValidator<CreateRecipe>
    {
        public CreateRecipeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(200)
                .Matches(RegularExpressions.WholeSentence)
                .WithMessage("Name must starts with the capital letter and can contains only letters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(50)
                .MaximumLength(1000);

			RuleFor(x => x.Components)
				.NotEmpty();

			RuleForEach(x => x.Components)
				.SetValidator(new CreateComponentValidator());

			RuleFor(x => x.IsLactoseFree)
                .NotNull();

            RuleFor(x => x.IsGlutenFree)
                .NotNull();

            RuleFor(x => x.IsVegan)
                .NotNull();

            RuleFor(x => x.IsVegetarian)
                .NotNull();
        }
    }
}