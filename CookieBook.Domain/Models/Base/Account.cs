using System;

namespace CookieBook.Domain.Models.Base
{
    /// <summary>
    /// Base class for account-like models.
    /// <para>Represends a container-like table for all accounts.</para>
    /// </summary>
    public abstract class Account : Entity
    {
        public string Nick { get; set; }
        public UInt64 Login { get; set; }
        public byte[] Salt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Role { get; set; }
        public string RestoreKey { get; set; }
        public bool IsActive { get; set; }
        public bool IsRestoreKeyFresh { get; set; }
        public DateTime? RestoreKeyUsedAt { get; set; }

        public Account() : base()
        {
            IsActive = true;
            IsRestoreKeyFresh = true;
        }

        public Account(string nick, UInt64 login, byte[] salt, byte[] passwordHash, string restoreKey) : this()
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

        public void UpdatePassword(byte[] newPassword)
        {
            PasswordHash = newPassword;
            base.Update();
        }
    }
}