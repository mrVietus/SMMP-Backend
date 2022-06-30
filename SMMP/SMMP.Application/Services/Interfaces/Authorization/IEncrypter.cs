using System;

namespace SMMP.Application.Services.Interfaces.Authorization
{
    public interface IEncrypter
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}
