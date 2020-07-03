using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Extensions
{
    public class RandomStringGenerator
    {
        public static string GenerateUnique()
        {
            var guid = Guid.NewGuid();
            var value = Convert.ToBase64String(guid.ToByteArray());

            return value.Replace("=", "").Replace("+", "").Replace("/", "");
        }
    }
}