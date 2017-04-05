/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NLog;
using NuGet.Protocol.Core.v3;
using OpenIddict.Core;
using OpenIddict.Models;
using WebApp.DataAccessLayer.Models;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("connect")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class OpenIdDictAuthorizationController : Controller
    {
        private readonly OpenIddictApplicationManager<OpenIddictApplication> applicationManager;
        private readonly IOptions<IdentityOptions> identityOptions;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Logger logger;

        public OpenIdDictAuthorizationController(
            OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
            IOptions<IdentityOptions> identityOptions,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            this.applicationManager = applicationManager;
            this.identityOptions = identityOptions;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("token")]
        [Produces("application/json")]
        public async Task<IActionResult> Token([FromBody] OpenIdConnectRequest request)
        {
            this.logger.Trace($"Token called with request: {request.ToJson()}");
            if (request.IsPasswordGrantType())
            {
                return await this.IsPasswordGrantType(request);
            }

            if (request.IsClientCredentialsGrantType())
            {
                return await this.IsClientCredentialsGrantType(request);
            }

            this.logger.Error($"Error in Token: The specified grant ({request.GrantType}) type is not supported");
            return this.BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant type is not supported."
            });
        }

        private async Task<IActionResult> IsPasswordGrantType(OpenIdConnectRequest request)
        {
            this.logger.Trace($"Token GrantType is Password");
            var user = await this.userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                this.logger.Error($"Error in Token: The username/password couple is invalid");
                return this.BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "The username/password couple is invalid."
                });
            }

            // Ensure the user is allowed to sign in.
            if (!await this.signInManager.CanSignInAsync(user))
            {
                this.logger.Error($"Error in Token: The specified user ({user.ToJson()}) is not allowed to sign in");
                return this.BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "The specified user is not allowed to sign in."
                });
            }

            // Reject the token request if two-factor authentication has been enabled by the user.
            if (this.userManager.SupportsUserTwoFactor && await this.userManager.GetTwoFactorEnabledAsync(user))
            {
                this.logger.Error($"Error in Token: The specified user ({user.ToJson()}) is not allowed to sign in");
                return this.BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "The specified user is not allowed to sign in."
                });
            }

            // Ensure the user is not already locked out.
            if (this.userManager.SupportsUserLockout && await this.userManager.IsLockedOutAsync(user))
            {
                this.logger.Error($"Error in Token: The username/password couple is invalid");
                return this.BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "The username/password couple is invalid."
                });
            }

            // Ensure the password is valid.
            if (!await this.userManager.CheckPasswordAsync(user, request.Password))
            {
                if (this.userManager.SupportsUserLockout)
                {
                    await this.userManager.AccessFailedAsync(user);
                }

                this.logger.Error($"Error in Token: The username/password couple is invalid");
                return this.BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "The username/password couple is invalid."
                });
            }

            if (this.userManager.SupportsUserLockout)
            {
                await this.userManager.ResetAccessFailedCountAsync(user);
            }

            // Create a new authentication ticket.
            var ticket = await this.CreatePasswordGrantTypeTicketAsync(request, user);

            return this.SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }

        private async Task<AuthenticationTicket> CreatePasswordGrantTypeTicketAsync(OpenIdConnectRequest request, ApplicationUser user)
        {
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

            //ticket.SetResources("resource-server");

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.

            foreach (var claim in ticket.Principal.Claims)
            {
                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                if (claim.Type == this.identityOptions.Value.ClaimsIdentity.SecurityStampClaimType)
                {
                    continue;
                }

                var destinations = new List<string>
                {
                    OpenIdConnectConstants.Destinations.AccessToken
                };

                // Only add the iterated claim to the id_token if the corresponding scope was granted to the client application.
                // The other claims will only be added to the access_token, which is encrypted when using the default format.
                if ((claim.Type == OpenIdConnectConstants.Claims.Name && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Email && ticket.HasScope(OpenIdConnectConstants.Scopes.Email)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Role && ticket.HasScope(OpenIddictConstants.Claims.Roles)))
                {
                    destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
                }

                claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken);
            }

            return ticket;
        }

        private async Task<IActionResult> IsClientCredentialsGrantType(OpenIdConnectRequest request)
        {
            this.logger.Trace($"Token GrantType is Client");
            // Note: the client credentials are automatically validated by OpenIddict:
            // if client_id or client_secret are invalid, this action won't be invoked.

            var application =
                await this.applicationManager.FindByClientIdAsync(request.ClientId, this.HttpContext.RequestAborted);
            if (application == null)
            {
                this.logger.Error($"Error in Token: The client application ({request.ClientId}) was not found in the database");
                return this.BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidClient,
                    ErrorDescription = "The client application was not found in the database."
                });
            }

            // Create a new authentication ticket.
            var ticket = this.CreateClientCredentialsGrantTypeTicket(request, application);

            return this.SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }
        private AuthenticationTicket CreateClientCredentialsGrantTypeTicket(OpenIdConnectRequest request, OpenIddictApplication application)
        {
            // Create a new ClaimsIdentity containing the claims that
            // will be used to create an id_token, a token or a code.
            var identity = new ClaimsIdentity(
                OpenIdConnectServerDefaults.AuthenticationScheme,
                OpenIdConnectConstants.Claims.Name,
                OpenIdConnectConstants.Claims.Role);

            // Use the client_id as the subject identifier.
            identity.AddClaim(OpenIdConnectConstants.Claims.Subject, application.ClientId,
                OpenIdConnectConstants.Destinations.AccessToken,
                OpenIdConnectConstants.Destinations.IdentityToken);

            identity.AddClaim(OpenIdConnectConstants.Claims.Name, application.DisplayName,
                OpenIdConnectConstants.Destinations.AccessToken,
                OpenIdConnectConstants.Destinations.IdentityToken);

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(
                new ClaimsPrincipal(identity),
                new AuthenticationProperties(),
                OpenIdConnectServerDefaults.AuthenticationScheme);

            //ticket.SetResources("resource_server");

            return ticket;
        }
    }
}