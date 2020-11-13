namespace CookieBook.Infrastructure.Commands.Category
{
    /// <summary>
    /// Represents a set of data used to create new category
    /// </summary>
    public class CreateCategory
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        /// <example>Fast Food</example>
        public string Name { get; set; }
    }
}