using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class RecipeImageDto : ImageDto
    {
        /// <summary>
        /// Id of the recipe that image belongs to
        /// </summary>
        /// <example>2</example>
		public int? RecipeRef { get; set; }
    }
}