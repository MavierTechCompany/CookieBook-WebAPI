using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Extensions
{
    public static class ValidationExtensions
    {
        public static Func<float, bool> IsDivisibleByZeroCommaFive = (float value) => value % 0.5 == 0;

        public static Func<string, bool> IsValidUnsignedLongValue = (string dataHash) =>
        {
            try
            {
                var val = ulong.Parse(dataHash);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        };

        public static Func<DateTime, bool> IsClientUtcDateOlderThanOrEqual = (DateTime date) =>
        {
            var utcKindDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);

            return utcKindDate.Date <= DateTime.UtcNow.Date;
        };
    }
}