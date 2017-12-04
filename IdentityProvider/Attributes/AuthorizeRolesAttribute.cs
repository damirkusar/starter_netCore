using Microsoft.AspNetCore.Authorization;

namespace IdentityProvider.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.Roles = string.Join(",", roles);
        }
    }
}