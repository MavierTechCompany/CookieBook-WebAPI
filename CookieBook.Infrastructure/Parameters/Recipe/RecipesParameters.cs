using System;

namespace CookieBook.Infrastructure.Parameters.Recipe
{
    public class RecipesParameters : BaseParameters
    {
        /// <summary>
        /// Date of recipe creation
        /// </summary>
        /// <example>2020-10-30T12:00:00.005Z</example>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is lactose free. Skipped if null or empty
        /// </summary>
        /// <example>True</example>
        public bool? IsLactoseFree { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is gluten free. Skipped if null or empty
        /// </summary>
        /// <example>True</example>
        public bool? IsGlutenFree { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is good for vegans. Skipped if null or empty
        /// </summary>
        /// <example>True</example>
        public bool? IsVegan { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is good for vegetarians. Skipped if null or empty
        /// </summary>
        /// <example>True</example>
        public bool? IsVegetarian { get; set; }
    }
}