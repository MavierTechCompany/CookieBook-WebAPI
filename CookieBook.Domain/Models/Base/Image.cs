using System;

namespace CookieBook.Domain.Models.Base
{
    /// <summary>
    /// Base class for image-like models.
    /// <para>Represends a container-like table for all images.</para>
    /// </summary>
    public abstract class Image : Entity
    {
        public string ImageContent { get; set; }

        public Image() : base() { }

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