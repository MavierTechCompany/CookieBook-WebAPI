using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Validators.User
{
    public class BlockUserValidator : AbstractValidator<BlockUser>
    {
        public BlockUserValidator()
        {
            RuleFor(req => req.Login)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => ValidationExtensions.IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");

            RuleFor(req => req.Email)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => ValidationExtensions.IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");
        }
    }
}