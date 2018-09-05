using System;

namespace CookieBook.Domain.Models.Base
{
    public abstract class Image
    {
        public int Id { get; protected set; }
        public string ImageContent { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public Image() { }

        public Image(string content)
        {
            ImageContent = content;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual void Update(string content)
        {
            ImageContent = content;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}