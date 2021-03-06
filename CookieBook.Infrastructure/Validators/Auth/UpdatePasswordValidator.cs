using System;
using CookieBook.Infrastructure.Commands.Account;
using FluentValidation;

namespace CookieBook.Infrastructure.Validators.Auth
{
    public class UpdatePasswordValidator : AbstractValidator<UpdatePassword>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(req => req.Password)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");

            RuleFor(req => req.NewPassword)
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
                ulong val = ulong.Parse(dataHash);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        };
    }
}