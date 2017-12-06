using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Identity.Data.Models;
using Identity.Interface.Constants;
using Identity.Interface.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Core;

namespace Identity.Services
{
    public class Auth : IAuth
    {
        private readonly ILogger<Auth> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public Auth(
            ILogger<Auth> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> IsUserValidToSignInAsync(string userName, string password)
        {
            var user = await this.GetApplicationUserByUsernameAsync(userName);
            if (user == null)
            {
                return false;
            }

            // Validate the username/password parameters and ensure the account is not locked out.
            var result = await this.signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<AuthenticationTicket> CreatePasswordGrantTypeTicketAsync(OpenIdConnectRequest request, IOptions<IdentityOptions> identityOptions)
        {
            var principal = await this.CreateUserPrincipal(request);

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(principal,
                new AuthenticationProperties(),
                OpenIdConnectServerDefaults.AuthenticationScheme);

            // Set the list of scopes granted to the client application.
            ticket.SetScopes(new[]
            {
                OpenIdConnectConstants.Scopes.OpenId,
                OpenIdConnectConstants.Scopes.Email,
                OpenIdConnectConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.Roles
            }.Intersect(request.GetScopes()));

            ticket.SetResources("resource-server");

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.
            var destinations = new List<string>
            {
                OpenIdConnectConstants.Destinations.AccessToken
            };

            foreach (var claim in ticket.Principal.Claims)
            {
                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                if (claim.Type == identityOptions.Value.ClaimsIdentity.SecurityStampClaimType)
                {
                    continue;
                }

                // Only add the iterated claim to the id_token if the corresponding scope was granted to the client application.
                // The other claims will only be added to the access_token, which is encrypted when using the default format.
                if (claim.Type == OpenIdConnectConstants.Claims.Username && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile) ||
                    claim.Type == Claims.LastName && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile) ||
                    claim.Type == Claims.LastName && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile) ||
                    claim.Type == Claims.Email && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile) ||
                    claim.Type == OpenIdConnectConstants.Claims.Role && ticket.HasScope(OpenIddictConstants.Scopes.Roles))
                {
                    destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
                }

                claim.SetDestinations(destinations);
            }

            return ticket;
        }

        private async Task<ClaimsPrincipal> CreateUserPrincipal(OpenIdConnectRequest request)
        {
            var user = await this.GetApplicationUserByUsernameAsync(request.Username);
            var principal = await this.signInManager.CreateUserPrincipalAsync(user);

            await this.CreateClaim(user, principal, Claims.LastName, user.LastName);
            await this.CreateClaim(user, principal, Claims.FirstName, user.FirstName);
            await this.CreateClaim(user, principal, Claims.Email, user.Email);

            var newPrincipal = await this.signInManager.CreateUserPrincipalAsync(user);
            return newPrincipal;
        }

        private async Task CreateClaim(ApplicationUser user, ClaimsPrincipal principal, string claimKey, string claimValue)
        {
            var currentFirstNameClaim = principal.GetClaim(claimKey);
            if (currentFirstNameClaim == null)
            {
                var newClaim = new Claim(claimKey, claimValue);
                await this.userManager.AddClaimAsync(user, newClaim);
            }
            else
            {
                var oldClaim = new Claim(claimKey, currentFirstNameClaim);
                var newClaim = new Claim(claimKey, claimValue);

                await this.userManager.ReplaceClaimAsync(user, oldClaim, newClaim);
            }
        }

        private async Task<ApplicationUser> GetApplicationUserByUsernameAsync(string username)
        {
            return await this.userManager.FindByNameAsync(username);
        }
    }
}