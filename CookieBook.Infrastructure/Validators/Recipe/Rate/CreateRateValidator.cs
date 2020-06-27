using CookieBook.Infrastructure.Commands.Recipe.Rate;
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
                .Must(x => IsDivisibleByZeroCommaFive(x))
                .WithMessage("Value must be divisible by 0.5");

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }

        private Func<float, bool> IsDivisibleByZeroCommaFive = (float value) => value % 0.5 == 0;
    }
}