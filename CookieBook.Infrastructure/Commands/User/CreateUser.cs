using CookieBook.Infrastructure.Commands.Account;

namespace CookieBook.Infrastructure.Commands.User
{
    public class CreateUser : CreateAccount
    {
        public ulong UserEmail { get; set; }
    }
}