using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Commands.Recipe.Rate
{
    public class CreateRate
    {
        public float Value { get; set; }
        public string Description { get; set; }
    }
}