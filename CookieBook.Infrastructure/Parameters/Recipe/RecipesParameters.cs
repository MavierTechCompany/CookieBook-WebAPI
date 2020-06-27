using System;

namespace CookieBook.Infrastructure.Parameters.Recipe
{
    public class RecipesParameters : BaseParameters
    {
        public DateTime CreatedAt { get; set; }
        public bool? IsLactoseFree { get; set; }
        public bool? IsGlutenFree { get; set; }
        public bool? IsVegan { get; set; }
        public bool? IsVegetarian { get; set; }
    }
}