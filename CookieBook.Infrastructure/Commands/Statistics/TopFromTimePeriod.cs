using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Commands.Statistics
{
    public class TopFromTimePeriod : TimePeriod
    {
        /// <summary>
        /// The number of top users to return.
        /// </summary>
        /// <example>10</example>
        public int Amount { get; set; }
    }
}