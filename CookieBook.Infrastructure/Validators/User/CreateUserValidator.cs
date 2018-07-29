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
                .Matches(@"^[A-Za-z][A-Za-z0-9_-]+$");

            RuleFor(req => req.Login)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Matches(@"^[1-9]\d[0-9]\d+$");

            RuleFor(req => req.Password)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Matches(@"^[1-9]\d[0-9]\d+$");

            RuleFor(req => req.UserEmail)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Matches(@"^[1-9]\d[0-9]\d+$");
        }
    }
}