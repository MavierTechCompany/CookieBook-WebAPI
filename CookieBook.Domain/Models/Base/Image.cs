using System;

namespace CookieBook.Domain.Models.Base
{
    public abstract class Image : Entity
    {
        public string ImageContent { get; protected set; }

        public Image() { }

        public Image(string content) : base()
        {
            ImageContent = content;
        }

        public virtual void Update(string content)
        {
            ImageContent = content;
            base.Update();
        }
    }
}