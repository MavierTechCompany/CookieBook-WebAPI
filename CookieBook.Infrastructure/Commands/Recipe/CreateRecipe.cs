using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CookieBook.Infrastructure.Commands.Recipe.Component;

namespace CookieBook.Infrastructure.Commands.Recipe
{
    public class CreateRecipe
    {
        /// <summary>
        /// Name of the recipe
        /// </summary>
        /// <example>Classic Hot-Dog</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description of the recipe. Contains steps to do and other text related to the meal preparation
        /// </summary>
        /// <example>1. Cook sausage. | 2. Bake the roll.</example>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is lactose free
        /// </summary>
        /// <example>False</example>
        [Required]
        public bool IsLactoseFree { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is gluten free
        /// </summary>
        /// <example>False</example>
        [Required]
        public bool IsGlutenFree { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is good for vegans
        /// </summary>
        /// <example>False</example>
        [Required]
        public bool IsVegan { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is good for vegetarians
        /// </summary>
        /// <example>True</example>
        [Required]
        public bool IsVegetarian { get; set; }

        /// <summary>
        /// List of components/ingredients for this recipe
        /// </summary>
        [Required]
        public List<CreateComponent> Components { get; set; }
    }
}