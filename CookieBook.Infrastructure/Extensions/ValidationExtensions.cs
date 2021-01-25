using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Extensions
{
    public static class ValidationExtensions
    {
        public static Func<float, bool> IsDivisibleByZeroCommaFive = (float value) => value % 0.5 == 0;

        public static Func<DateTime, bool> IsClientUtcDateOlderThanOrEqual = (DateTime date) => DateTime.SpecifyKind(date, DateTimeKind.Utc).Date <= DateTime.UtcNow.Date;

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
    }
}