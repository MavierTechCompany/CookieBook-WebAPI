using System;
using CookieBook.Infrastructure.Commands.User;
using FluentValidation;

namespace CookieBook.Infrastructure.Validators.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserData>
    {
        public UpdateUserValidator()
        {
            RuleFor(req => req.Login)
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