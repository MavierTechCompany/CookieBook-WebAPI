using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Parameters.Recipe.Rate
{
    public class RatesParameters : BaseParameters
    {
        /// <summary>
        /// The creation date you want to search for
        /// </summary>
        /// <example>2020-02-02</example>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Rate value you want to search for
        /// </summary>
        /// <example>3.5</example>
        public float? Value { get; set; }
    }
}