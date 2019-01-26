using System;

namespace CookieBook.Domain.Models.Base
{
    /// <summary>
    /// Base class for models that represents intermeiate tables.
    /// </summary>
    public class IntermediateEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IntermediateEntity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}