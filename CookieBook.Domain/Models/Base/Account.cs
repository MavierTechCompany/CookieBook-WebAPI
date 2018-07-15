namespace CookieBook.Domain.Models.Base
{
    public class Account
    {
        public int Id { get; private set; }
        public string Nick { get; private set; }
        public string Login { get; private set; }
        public string Email { get; private set; }
        public byte[] Salt { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public string Role { get; private set; }

        public Account() { }
    }
}