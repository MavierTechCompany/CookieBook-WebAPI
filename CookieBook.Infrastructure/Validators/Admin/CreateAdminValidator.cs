using CookieBook.Infrastructure.Commands.Admin;
using CookieBook.Infrastructure.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Validators.Admin
{
    public class CreateAdminValidator : AbstractValidator<CreateAdmin>
    {
        public CreateAdminValidator()
        {
            RuleFor(req => req.Nick)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20)
                .Matches(RegularExpressions.Nick)
                .WithMessage(@"Nick must starts with the letter and can contains only letters and digits.");

            RuleFor(req => req.Password)
                .NotEmpty()
                .MinimumLength(19)
                .MaximumLength(20)
                .Must(a => ValidationExtensions.IsValidUnsignedLongValue(a))
                .WithMessage("Invalid value!");
        }
    }
}