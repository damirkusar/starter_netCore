using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Core;
using WebApp.Identity.DataAccessLayer.Models;
using WebApp.Identity.Interface;

namespace WebApp.Identity
{
    public class AuthService: IAuthService
    {
        private readonly ILogger<AuthService> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(
            ILogger<AuthService> logger,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.logger = logger;
            this.mapper = mapper;
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
            ApplicationUser user = await this.GetApplicationUserByUsernameAsync(request.Username);
            // Create a new ClaimsPrincipal containing the claims that
            // will be used to create an id_token, a token or a code.
            var principal = await this.signInManager.CreateUserPrincipalAsync(user);

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
                OpenIdConnectConstants.Destinations.AccessToken,
                OpenIdConnectConstants.Destinations.IdentityToken
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
                if (claim.Type == OpenIdConnectConstants.Claims.Name && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile) ||
                    claim.Type == OpenIdConnectConstants.Claims.Email && ticket.HasScope(OpenIdConnectConstants.Scopes.Email) ||
                    claim.Type == OpenIdConnectConstants.Claims.Role && ticket.HasScope(OpenIddictConstants.Scopes.Roles))
                {
                    destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
                }

                claim.SetDestinations(destinations);
            }

            return ticket;
        }

        private async Task<ApplicationUser> GetApplicationUserByUsernameAsync(string username)
        {
            return await this.userManager.FindByNameAsync(username);
        }
    }
}