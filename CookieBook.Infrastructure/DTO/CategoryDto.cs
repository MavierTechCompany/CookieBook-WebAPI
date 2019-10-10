using System.Collections.Generic;
using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class CategoryDto : EntityDto
    {
		public string Name { get; set; }
		public virtual ICollection<RecipeDto> Recipes { get; set; }
    }
}