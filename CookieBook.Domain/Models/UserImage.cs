using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
    public class UserImage : Image
    {
        public int? UserRef { get; set; }

        public User User { get; set; }

        public UserImage() : base() { }

        public UserImage(string content) : base(content) { }
    }
}