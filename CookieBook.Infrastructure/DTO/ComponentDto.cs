using CookieBook.Domain.Enums;
using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class ComponentDto : EntityDto
    {
        /// <summary>
        /// Name of the ingridient
        /// </summary>
        /// <example>Sausage</example>
        public string Name { get; set; }

        /// <summary>
        /// Ingridient quantity unit number
        /// </summary>
        /// <example>5</example>
        public Unit Unit { get; set; }

        /// <summary>
        /// Ingridient quantity
        /// </summary>
        /// <example>1</example>
        public float Amount { get; set; }

        /// <summary>
        /// Id of the recipe that this component belongs to
        /// </summary>
        /// <example>2</example>
        public int RecipeId { get; set; }
    }
}