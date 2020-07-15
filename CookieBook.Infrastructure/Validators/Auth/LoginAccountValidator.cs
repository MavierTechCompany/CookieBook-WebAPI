using System;
using CookieBook.Infrastructure.Commands.Auth;
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
                .Must(a => IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");

            RuleFor(req => req.Password)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");
        }

        Func<string, bool> IsValidUnsignedLongValue = (string dataHash) =>
        {
            try
            {
                ulong val = ulong.Parse(dataHash);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        };
    }
}