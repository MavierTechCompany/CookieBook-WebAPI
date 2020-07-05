using System;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Extensions;
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
                .Matches(RegularExpressions.Nick)
                .WithMessage(@"Nick must starts with the letter and can contains only letters and digits.");

            RuleFor(req => req.Login)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");

            RuleFor(req => req.Password)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");

            RuleFor(req => req.UserEmail)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");
        }

        private Func<string, bool> IsValidUnsignedLongValue = (string dataHash) =>
        {
            try
            {
                var val = ulong.Parse(dataHash);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        };
    }
}