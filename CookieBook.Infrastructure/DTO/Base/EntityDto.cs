using System;

namespace CookieBook.Infrastructure.DTO.Base
{
    public class EntityDto
    {
        /// <summary>
        /// Id of the entity
        /// </summary>
        /// <example>8</example>
		public int Id { get; set; }

        /// <summary>
        /// Entity creation date
        /// </summary>
		public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Entity update date
        /// </summary>
		public DateTime UpdatedAt { get; set; }
    }
}