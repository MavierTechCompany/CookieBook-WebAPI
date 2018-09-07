using CookieBook.Infrastructure.Commands.Account;

namespace CookieBook.Infrastructure.Commands.User
{
    public class UpdateUserData : UpdateAccountData
    {
        public string UserEmail { get; set; }
    }
}