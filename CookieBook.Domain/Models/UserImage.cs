using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
    public class UserImage : Image
    {
        public int? UserId { get; set; }

        public virtual User User { get; set; }

        public UserImage() : base()
        {
        }

        public UserImage(string content) : base(content)
        {
        }
    }
}