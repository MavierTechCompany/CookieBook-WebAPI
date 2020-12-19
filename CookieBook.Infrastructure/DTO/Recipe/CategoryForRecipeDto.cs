using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class CategoryForRecipeDto : EntityDto
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        /// <example>Fast-Food</example>
		public string Name { get; set; }
    }
}