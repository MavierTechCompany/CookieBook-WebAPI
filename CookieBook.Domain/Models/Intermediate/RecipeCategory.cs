using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models.Intermediate
{
    public class RecipeCategory : IntermediateEntity
    {
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }

        public Recipe Recipe { get; set; }
        public Category Category { get; set; }

        public RecipeCategory() : base() { }
    }
}