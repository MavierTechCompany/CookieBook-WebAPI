using System.Collections.Generic;
using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}