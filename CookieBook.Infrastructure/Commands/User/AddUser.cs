using CookieBook.Infrastructure.Commands.Account;

namespace CookieBook.Infrastructure.Commands.User
{
    public class AddUser : AddAccount
    {
        public string UserEmail { get; set; }
    }
}