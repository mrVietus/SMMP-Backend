using System;
using System.Security.Cryptography;
using SMMP.Application.Services.Interfaces.Authorization;
using SMMP.Core.Extensions;


namespace SMMP.Application.Services.Implementation.Authorization
{
    public class Encrypter : IEncrypter
    {
        private static readonly int DeriveBytesIterationsCount = 10000;
        private static readonly int SaltSize = 40;

        public string GetSalt(string value)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException("Can not generate salt from an empty value.", nameof(value));
            }

            var saltBytes = new byte[SaltSize];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string value, string salt)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException("Can not generate hash from an empty value.", nameof(value));
            }
            if (salt.IsNullOrEmpty())
            {
                throw new ArgumentException("Can not use an empty salt from hashing value.", nameof(value));
            }

            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        private byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
