using System;

namespace CookieBook.Domain.Models.Base
{
    public class Image
    {
        public int Id { get; private set; }
        public byte[] ImageContent { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Image() { }
    }
}