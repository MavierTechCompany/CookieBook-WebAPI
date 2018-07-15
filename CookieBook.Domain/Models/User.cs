using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
    public class User : Account
    {
        public int UserImageId { get; private set; }
        public UserImage UserImage { get; set; }

        public User() { }
    }
}