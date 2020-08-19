using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace eMTE.Common.Authentication
{
    public class UserResolver : IUserResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserResolver(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetClaimIdentifierValue(string identifier)
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims.Single(cl => cl.Type == identifier);
            return claim.Value;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier);
            return userId.Value;
        }

        public string TryGetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            return userId?.Value;
        }
    }
}
