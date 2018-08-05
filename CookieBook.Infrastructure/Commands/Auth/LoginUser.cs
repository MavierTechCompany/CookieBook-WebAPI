namespace CookieBook.Infrastructure.Commands.Auth
{
    public class LoginUser
    {
        public ulong LoginOrEmail { get; set; }
        public ulong Password { get; set; }
    }
}