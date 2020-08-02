using System;
using eMTE.Common.Authentication.Models;

namespace eMTE.Common.Authentication
{
    public interface IAuthenticator
    {
        AuthenticateModel Create(string secret, string password);
        bool Verify(string password, string hash);
    }
}
