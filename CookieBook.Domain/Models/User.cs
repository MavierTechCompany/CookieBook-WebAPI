using System;
using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
    public class User : Account
    {
        public UInt64 UserEmail { get; protected set; }
        public int? UserImageId { get; private set; }
        public virtual UserImage UserImage { get; set; }

        public User() { }

        public User(string nick, UInt64 login, byte[] salt, byte[] passwordHash,
            string restoreKey, UInt64 userEmail) : base(nick, login, salt, passwordHash, restoreKey)
        {
            UserEmail = userEmail;
            Role = "user";
        }

        public void Update(UInt64 login, UInt64 userEmail)
        {
            Login = login;
            UserEmail = userEmail;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}