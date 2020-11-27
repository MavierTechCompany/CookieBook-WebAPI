using System.Collections.Generic;
using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class CategoryDto : EntityDto
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        /// <example>Fast Food</example>
        public string Name { get; set; }

        /// <summary>
        /// Recipes that belong to the given category.
        /// </summary>
        public virtual ICollection<RecipeForCategoryDto> Recipes { get; set; }
    }
}