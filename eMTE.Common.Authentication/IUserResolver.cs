using System;
namespace eMTE.Common.Authentication
{
    public interface IUserResolver
    {
        string TryGetUserId();
        string GetUserId();
        string GetClaimIdentifierValue(string identifier);
    }
}
