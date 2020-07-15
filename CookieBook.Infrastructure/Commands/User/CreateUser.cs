using CookieBook.Infrastructure.Commands.Account;

namespace CookieBook.Infrastructure.Commands.User
{
    public class CreateUser : CreateAccount
    {
        public string Login { get; set; }
        public string UserEmail { get; set; }
    }
}