using CookieBook.Infrastructure.Commands.Category;
using CookieBook.Infrastructure.Extensions;
using FluentValidation;

namespace CookieBook.Infrastructure.Validators.Category
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategory>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(150)
				.Matches(RegularExpressions.WholeSentence)
				.WithMessage("Name must starts with the capital letter and can contains only letters.");
        }
    }
}