using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Extensions
{
    public class ValueInTime<T>
    {
        public T Value { get; set; }
        public DateTime Date { get; set; }
    }
}