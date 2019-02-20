using System;

namespace CookieBook.Domain.Models.Base
{
    /// <summary>
    /// Base class for all database models.
    /// </summary>
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Entity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates UpdatedAt property with the current time in the UTC format.
        /// </summary>
        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}