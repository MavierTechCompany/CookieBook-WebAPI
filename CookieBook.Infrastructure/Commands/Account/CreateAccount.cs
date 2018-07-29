namespace CookieBook.Infrastructure.Commands.Account
{
    public class CreateAccount
    {
        public string Nick { get; set; }
        public ulong Login { get; set; }
        public ulong Password { get; set; }
    }
}