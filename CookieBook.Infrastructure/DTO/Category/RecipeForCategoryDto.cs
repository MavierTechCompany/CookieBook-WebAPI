using System.Collections.Generic;
using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class RecipeForCategoryDto : EntityDto
    {
        /// <summary>
        /// Name of the recipe
        /// </summary>
        /// <example>Classic Hot-Dog</example>
        public string Name { get; set; }

        /// <summary>
        /// Description of the recipe. Contains steps to do and other text related to the meal preparation
        /// </summary>
        /// <example>1. Cook sausage. | 2. Bake the roll.</example>
        public string Description { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is lactose free
        /// </summary>
        /// <example>False</example>
        public bool IsLactoseFree { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is gluten free
        /// </summary>
        /// <example>False</example>
        public bool IsGlutenFree { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is good for vegans
        /// </summary>
        /// <example>False</example>
        public bool IsVegan { get; set; }

        /// <summary>
        /// Indicates whether or not this meal from this recipe is good for vegetarians
        /// </summary>
        /// <example>True</example>
        public bool IsVegetarian { get; set; }

        /// <summary>
        /// The id of the user who created the recipe
        /// </summary>
        /// <example>3</example>
        public int UserId { get; set; }

        /// <summary>
        /// Image included to this recipe
        /// </summary>
        public virtual RecipeImageDto RecipeImage { get; set; }

        /// <summary>
        /// List of components/ingredients for this recipe
        /// </summary>
        public virtual ICollection<ComponentDto> Components { get; set; }
    }
}