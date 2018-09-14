using System;

namespace CookieBook.Infrastructure.Extensions.Security.Interface
{
    public interface IDataHashManager
    {
        void CalculatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        void CalculatePasswordHash(string password, byte[] passwordSalt, out byte[] passwordHash);
        UInt64 CalculateDataHash(string data);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        bool VerifyDataHash(string data, UInt64 dataHash);
    }
}