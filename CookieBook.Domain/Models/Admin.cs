using CookieBook.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Domain.Models
{
    public class Admin : Account
    {
        public Admin() : base()
        {
        }

        public Admin(string nick, ulong login, byte[] salt, byte[] passwordHash, string restoreKey) : base(nick, login, salt, passwordHash, restoreKey) => Role = "admin";
    }
}