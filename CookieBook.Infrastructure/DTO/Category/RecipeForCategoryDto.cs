using System.Collections.Generic;
using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class RecipeForCategoryDto : EntityDto
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsLactoseFree { get; set; }
		public bool IsGlutenFree { get; set; }
		public bool IsVegan { get; set; }
		public bool IsVegetarian { get; set; }
		public int UserId { get; set; }
		public virtual RecipeImageDto RecipeImage { get; set; }
		public virtual ICollection<ComponentDto> Components { get; set; }
    }
}