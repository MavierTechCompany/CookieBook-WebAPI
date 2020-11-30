using CookieBook.Infrastructure.DTO.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.DTO
{
    public class RateDto : EntityDto
    {
        /// <summary>
        /// Value of the rating
        /// </summary>
        /// <example>3.5</example>
        public float Value { get; set; }

        /// <summary>
        /// Id of the recipe that this rate belongs to
        /// </summary>
        /// <example>2</example>
        public int RecipeId { get; set; }

        /// <summary>
        /// Description/comment wrote by user that gave this rate
        /// </summary>
        /// <example>Tasty and simple, but I would add more vegetables.</example>
        public string Description { get; set; }
    }
}