using System;
using System.Collections;
using System.Collections.Generic;
using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
    public class User : Account
    {
        public UInt64 UserEmail { get; set; }
        public UserImage UserImage { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }

        public User() { }

        public User(string nick, UInt64 login, byte[] salt, byte[] passwordHash,
            string restoreKey, UInt64 userEmail) : base(nick, login, salt, passwordHash, restoreKey)
        {
            UserEmail = userEmail;
            Role = "user";
        }

        public void Update(UInt64 login, UInt64 userEmail)
        {
            UserEmail = userEmail;
			base.Update(login);
		}

        public void UpdatePassword(byte[] newPassword)
        {
            PasswordHash = newPassword;
			base.Update();
		}
    }
}