namespace CookieBook.Domain.Models.Base
{
    public abstract class Account
    {
        public int Id { get; protected set; }
        public string Nick { get; protected set; }
        public string Login { get; protected set; }
        public string Email { get; protected set; }
        public byte[] Salt { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
        public string Role { get; protected set; }
        public string RestoreKey { get; protected set; }

        public Account() { }
    }
}