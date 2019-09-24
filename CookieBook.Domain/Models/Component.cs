using System.Collections.Generic;
using CookieBook.Domain.Enums;
using CookieBook.Domain.Models.Base;
using CookieBook.Domain.Models.Intermediate;

namespace CookieBook.Domain.Models
{
    public class Component : Entity
    {
        public string Name { get; set; }
        public Unit Unit { get; set; }
        public float Amount { get; set; }

        public virtual Recipe Recipe { get; set; }

        public Component() : base() { }

        public Component(string name, Unit unit, float amount) : base()
        {
			Name = name;
			Unit = unit;
			Amount = amount;
		}

        public void Update(string name, Unit unit, float amount)
        {
            Name = name;
            Unit = unit;
            Amount = amount;
            base.Update();
        }
    }
}