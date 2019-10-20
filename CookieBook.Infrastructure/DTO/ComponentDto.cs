using CookieBook.Domain.Enums;
using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class ComponentDto : EntityDto
    {
		public string Name { get; set; }
		public Unit Unit { get; set; }
		public float Amount { get; set; }
		public int RecipeId { get; set; } 
    }
}