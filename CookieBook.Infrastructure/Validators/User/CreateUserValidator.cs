using CookieBook.Infrastructure.Commands.User;
using FluentValidation;

namespace CookieBook.Infrastructure.Validators.User
{
	public class CreateUserValidator : AbstractValidator<CreateUser>
	{
		public CreateUserValidator()
		{
			RuleFor(req => req.Nick)
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(20)
				.Matches(@"^[A-Za-z][A-Za-z0-9_-]+$")
				.WithMessage(@"Nick must starts with the letter and can contains only letters, digits, '_' and '-'.");

			RuleFor(req => req.Login)
				.NotEmpty();

			RuleFor(req => req.Password)
				.NotEmpty();

			RuleFor(req => req.UserEmail)
				.NotEmpty();
		}
	}
}