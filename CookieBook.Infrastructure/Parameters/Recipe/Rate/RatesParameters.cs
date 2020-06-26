using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Parameters.Recipe.Rate
{
    public class RatesParameters : BaseParameters
    {
        public DateTime CreatedAt { get; set; }
        public float? Value { get; set; }
    }
}