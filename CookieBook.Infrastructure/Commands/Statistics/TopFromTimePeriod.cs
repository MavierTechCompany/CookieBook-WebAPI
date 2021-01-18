using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Commands.Statistics
{
    public class TopFromTimePeriod : TimePeriod
    {
        public int Amount { get; set; }
    }
}