using System;
using System.Linq;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Primitives;
using Identity.Interface.Constants;

namespace Identity.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var guidClaim = GetClaimValue(principal, OpenIdConnectConstants.Claims.Subject);
            if (guidClaim.Equals(string.Empty))
            {
                return new Guid();
            }

            var guid = new Guid(guidClaim);
            return guid;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return GetClaimValue(principal, OpenIdConnectConstants.Claims.Username);
        }

        public static string GetLastName(this ClaimsPrincipal principal)
        {
            return GetClaimValue(principal, Claims.LastName);
        }

        public static string GetFirstName(this ClaimsPrincipal principal)
        {
            return GetClaimValue(principal, Claims.FirstName);
        }

        public static string GetClaimValue(ClaimsPrincipal principal, string claimKey)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            if (!principal.Claims.Any(claim => claim.Type.Equals(claimKey)))
            {
                return string.Empty;
            }

            var claimValue = principal.Claims.First(x => x.Type.Equals(claimKey)).Value;
            return claimValue;
        }
    }
}