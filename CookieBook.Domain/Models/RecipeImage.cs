using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
    public class RecipeImage : Image
    {
        public int? RecipeRef { get; set; }
        public Recipe Recipe { get; set; }

        public RecipeImage() : base() { }
        public RecipeImage(string content) : base(content) { }
    }
}