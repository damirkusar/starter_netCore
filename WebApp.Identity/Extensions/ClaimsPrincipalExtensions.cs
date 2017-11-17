using System;
using System.Linq;
using System.Security.Claims;

namespace WebApp.Identity.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            if (principal.Claims.Any())
            {
                var id = principal.Claims.First(x => x.Type.Equals("sub")).Value;
                var guid = new Guid(id);
                return guid;
            }
            return Guid.Empty;
        }

        public static string GetUsername(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            if (principal.Claims.Any())
            {
                var username = principal.Claims.First(x => x.Type.Equals("username")).Value;
                return username;
            }
            return string.Empty;
        }
    }
}