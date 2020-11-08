using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Commands.User
{
    public class UnblockUser
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string RestoreKey { get; set; }
    }
}