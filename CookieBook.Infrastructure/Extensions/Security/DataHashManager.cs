using System;
using System.Security.Cryptography;
using System.Text;

namespace CookieBook.Infrastructure.Extensions.Security
{
    public class DataHashManager : IDataHashManager
    {
        public void CalculatePasswordHash(string password, out byte[] passwordHash,
            out byte[] passwordSalt)
        {
            var hmac512 = new HMACSHA512();
            passwordSalt = hmac512.Key;
            passwordHash = hmac512.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public UInt64 CalculateDataHash(string data)
        {
            UInt64 hashedValue = 3074457345618258791ul;
            for (int i = 0; i < data.Length; i++)
            {
                hashedValue += data[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash,
            byte[] passwordSalt)
        {
            var hmac512 = new HMACSHA512();
            var computedHash = hmac512.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < passwordHash.Length; i++)
                if (computedHash[i] != passwordHash[i]) return false;
            return true;
        }

        public bool VerifyDataHash(string data, UInt64 dataHash)
        {
            var computedHash = CalculateDataHash(data);
            if (computedHash != dataHash) return false;
            return true;
        }
    }
}