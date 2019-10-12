using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class RecipeImageDto : ImageDto
    {
		public int? RecipeRef { get; set; }
    }
}