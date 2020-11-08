using CookieBook.Infrastructure.Commands.Recipe.Rate;
using CookieBook.Infrastructure.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Validators.Recipe.Rate
{
    public class CreateRateValidator : AbstractValidator<CreateRate>
    {
        public CreateRateValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5)
                .Must(x => ValidationExtensions.IsDivisibleByZeroCommaFive(x))
                .WithMessage("Value must be divisible by 0.5");

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}