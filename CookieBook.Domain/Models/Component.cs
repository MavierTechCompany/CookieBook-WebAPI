using System.Collections.Generic;
using CookieBook.Domain.Models.Base;
using CookieBook.Domain.Models.Intermediate;

namespace CookieBook.Domain.Models
{
    public class Component : Entity
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Amount { get; set; }

        public ICollection<RecipeComponent> RecipeComponents { get; set; }

        public Component() : base() { }
    }
}