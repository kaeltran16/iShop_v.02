using System;
using System.Security.Claims;

namespace iShop.Common.Extensions
{
    public static class IdentityUserExtensions
    {
        // Just an extension method for getting UserId
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return id.ToGuid();
        }
    }
}
