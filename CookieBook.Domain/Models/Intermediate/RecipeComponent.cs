using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models.Intermediate
{
    public class RecipeComponent : IntermediateEntity
    {
        public int RecipeId { get; set; }
        public int ComponentId { get; set; }

        public Recipe Recipe { get; set; }
        public Component Component { get; set; }

        public RecipeComponent() : base() { }
    }
}