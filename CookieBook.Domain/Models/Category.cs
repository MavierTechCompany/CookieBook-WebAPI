using System.Collections.Generic;
using CookieBook.Domain.Models.Base;
using CookieBook.Domain.Models.Intermediate;

namespace CookieBook.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<RecipeCategory> RecipeCategories { get; set; }

        public Category(string name) : base()
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
            base.Update();
        }
    }
}