using System;
namespace eMTE.Common.Authentication
{
    public interface IUserResolver
    {
        string GetUserId();
        string GetClaimIdentifierValue(string identifier);
    }
}
