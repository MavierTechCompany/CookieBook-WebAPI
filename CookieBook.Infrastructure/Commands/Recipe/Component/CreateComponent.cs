using CookieBook.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CookieBook.Infrastructure.Commands.Recipe.Component
{
    /// <summary>
    /// Set of data used to create recipe component/ingredient
    /// </summary>
    public class CreateComponent
    {
        /// <summary>
        /// Name of the component/ingredient
        /// </summary>
        /// <example>Sausage</example>
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Unit of measure
        /// </summary>
        /// <example>2</example>
        [Required]
        public Unit Unit { get; set; }

        /// <summary>
        /// Amount of the ingredient
        /// </summary>
        /// <example>2</example>
        [Required]
        [Range(0.5f, float.MaxValue)]
        public float Amount { get; set; }
    }
}