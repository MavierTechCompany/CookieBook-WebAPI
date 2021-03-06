using System;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Extensions;
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
                .Must(a => ValidationExtensions.IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");

            RuleFor(req => req.UserEmail)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => ValidationExtensions.IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");
        }
    }
}