using CookieBook.Infrastructure.Extensions;
using System.ComponentModel.DataAnnotations;

namespace CookieBook.Infrastructure.Commands.Category
{
    /// <summary>
    /// Represents a set of data used to update the category.
    /// </summary>
    public class UpdateCategory
    {
        /// <summary>
        /// A name of the category.
        /// </summary>
        /// <example>Fast Food</example>
        ///
        [Required]
        [MinLength(4)]
        [MaxLength(150)]
        [RegularExpression(RegularExpressions.WholeSentence)]
        public string Name { get; set; }
    }
}