namespace CookieBook.Infrastructure.Commands.Recipe
{
    public class UpdateRecipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLactoseFree { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
    }
}