using CookieBook.Domain.Enums;

namespace CookieBook.Infrastructure.Commands.Recipe.Component
{
    public class CreateComponent
    {
		public string Name { get; set; }
		public Unit Unit { get; set; }
		public float Amount { get; set; }
    }
}