using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Commands.User
{
    public class BlockUser
    {
        public string Login { get; set; }
        public string Email { get; set; }
    }
}