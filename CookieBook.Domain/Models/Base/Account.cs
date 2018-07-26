using System;

namespace CookieBook.Domain.Models.Base
{
    public abstract class Account
    {
        public int Id { get; protected set; }
        public string Nick { get; protected set; }
        public UInt64 Login { get; protected set; }
        public byte[] Salt { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
        public string Role { get; protected set; }
        public string RestoreKey { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public Account() { }

        public Account(string nick, UInt64 login, byte[] salt, byte[] passwordHash, string restoreKey)
        {
            Nick = nick;
            Login = login;
            Salt = salt;
            PasswordHash = passwordHash;
            RestoreKey = restoreKey;
            CreatedAt = DateTime.UtcNow;
        }
    }
}