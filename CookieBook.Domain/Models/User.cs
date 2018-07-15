using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
    public class User : Account
    {
        public Image UserImage { get; set; }

        public User() { }
    }
}