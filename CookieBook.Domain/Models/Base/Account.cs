using System;

namespace CookieBook.Domain.Models.Base
{
    /// <summary>
    /// Base class for account-like models.
    /// <para>Represends a container-like table for all accounts.</para>
    /// </summary>
    public abstract class Account : Entity
    {
        public string Nick { get; protected set; }
        public UInt64 Login { get; protected set; }
        public byte[] Salt { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
        public string Role { get; protected set; }
        public string RestoreKey { get; protected set; }

        public Account() : base() { }

        public Account(string nick, UInt64 login, byte[] salt, byte[] passwordHash, string restoreKey) : base()
        {
            Nick = nick;
            Login = login;
            Salt = salt;
            PasswordHash = passwordHash;
            RestoreKey = restoreKey;
        }

        public void Update(UInt64 login)
        {
            Login = login;
            base.Update();
        }
    }
}