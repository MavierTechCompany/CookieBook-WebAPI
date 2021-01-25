using CookieBook.Infrastructure.Commands.Statistics;
using CookieBook.Infrastructure.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Validators.Statistics
{
    public class TimePeriodValidator : AbstractValidator<TimePeriod>
    {
        public TimePeriodValidator()
        {
            RuleFor(x => x.StartDate)
                .NotNull()
                .NotEqual(default(DateTime));

            RuleFor(x => x.StartDate.Date)
                .LessThanOrEqualTo(x => x.EndDate.Date);

            RuleFor(x => x.EndDate)
                .NotNull()
                .NotEqual(default(DateTime))
                .Must(x => ValidationExtensions.IsClientUtcDateOlderThanOrEqual(x) == true)
                .WithMessage("'End Date' cannot be greater than today's date.");
        }
    }
}