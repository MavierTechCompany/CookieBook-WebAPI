using System;

namespace CookieBook.Domain.Models.Base
{
    /// <summary>
    /// Base class for all database models.
    /// </summary>
    public class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

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