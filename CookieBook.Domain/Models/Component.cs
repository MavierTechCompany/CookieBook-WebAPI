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

        public virtual ICollection<RecipeComponent> RecipeComponents { get; set; }

        public Component() : base() { }

        public void Update(string name, string unit, int amount)
        {
            Name = name;
            Unit = unit;
            Amount = amount;
            base.Update();
        }
    }
}