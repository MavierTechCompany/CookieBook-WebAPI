using System;
using System.Collections.Generic;
using System.Linq;

namespace CookieBook.Infrastructure.Extensions.Security
{
    public class PasswordGenerator
    {
        public int RequiredLength { get; private set; }
        public int RequiredUniqueChars { get; private set; }
        public bool RequireDigit { get; private set; }
        public bool RequireLowercase { get; private set; }
        public bool RequireNonAlphanumeric { get; private set; }
        public bool RequireUppercase { get; private set; }

        public static string GenerateRandomPassword(PasswordGenerator opts = null)
        {
            if (opts == null) opts = new PasswordGenerator()
            {
            RequiredLength = 12,
            RequiredUniqueChars = 5,
            RequireDigit = true,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true
            };

            string[] randomChars = new []
            {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",
                "abcdefghijkmnopqrstuvwxyz",
                "0123456789",
                "!@$?_-"
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count), randomChars[0][rand.Next(0,
                    randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count), randomChars[1][rand.Next(0,
                    randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count), randomChars[2][rand.Next(0,
                    randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count), randomChars[3][rand.Next(0,
                    randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength ||
                chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count), rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}