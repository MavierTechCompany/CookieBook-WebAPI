using System;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Extensions;
using FluentValidation;

namespace CookieBook.Infrastructure.Validators.Auth
{
    public class LoginAccountValidator : AbstractValidator<LoginAccount>
    {
        public LoginAccountValidator()
        {
            RuleFor(req => req.LoginOrEmail)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => ValidationExtensions.IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");

            RuleFor(req => req.Password)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => ValidationExtensions.IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");
        }
    }
}