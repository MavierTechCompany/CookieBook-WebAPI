using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookieBook.Infrastructure.Commands.Recipe.Rate
{
    public class CreateRate
    {
        /// <summary>
        /// Value of the rating
        /// </summary>
        /// <example>3.5</example>
        [Required]
        [Range(1, 5)]
        public float Value { get; set; }

        /// <summary>
        /// Description/comment wrote by user that gave this rate
        /// </summary>
        /// <example>Tasty and simple, but I would add more vegetables.</example>
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}