using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
    public class UserImage : Image
    {
        public User User { get; set; }

        public UserImage() : base() { }
    }
}