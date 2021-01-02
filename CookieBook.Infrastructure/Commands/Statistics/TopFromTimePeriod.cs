using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Commands.Statistics
{
    public class TopFromTimePeriod : TimePeriod
    {
        public uint Amount { get; set; }
    }
}