using System.Text;
using eMTE.Common.Authentication.Models;
using Isopoh.Cryptography.Argon2;

namespace eMTE.Common.Authentication
{
    public class Authenticator : IAuthenticator
    {
        public AuthenticateModel Create(string secret, string password)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            string passwordHash = Argon2.Hash(passwordBytes, secretBytes);
            return new AuthenticateModel
            {
                Hash = passwordHash,
                Password = password,
            };
        }

        public bool Verify(string password, string hash)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Argon2.Verify(hash, passwordBytes);
        }
    }
}
