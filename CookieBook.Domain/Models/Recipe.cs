using System.Collections.Generic;
using CookieBook.Domain.Models.Base;
using CookieBook.Domain.Models.Intermediate;

namespace CookieBook.Domain.Models
{
    public class Recipe : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLactoseFree { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }

        public virtual User User { get; set; }
        public virtual RecipeImage RecipeImage { get; set; }
        public virtual ICollection<RecipeCategory> RecipeCategories { get; set; }
        public virtual ICollection<RecipeComponent> RecipeComponents { get; set; }
    }
}