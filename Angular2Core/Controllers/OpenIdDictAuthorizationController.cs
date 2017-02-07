/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Angular2Core.Models;
using Angular2Core.ViewModels.Shared;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Core;
using OpenIddict.Models;

namespace Angular2Core.Controllers
{
    public class OpenIdDictAuthorizationController : Controller
    {
        private readonly OpenIddictApplicationManager<OpenIddictApplication> applicationManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public OpenIdDictAuthorizationController(
            OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            this.applicationManager = applicationManager;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        // Note: to support interactive flows like the code flow,
        // you must provide your own authorization endpoint action:

        [Authorize, HttpGet, Route("~/connect/authorize")]
        public async Task<IActionResult> Authorize(OpenIdConnectRequest request)
        {
            // Retrieve the application details from the database.
            var application = await this.applicationManager.FindByClientIdAsync(request.ClientId, CancellationToken.None);
            if (application == null)
            {
                return this.View("Error", new ErrorViewModel
                {
                    Error = OpenIdConnectConstants.Errors.InvalidClient,
                    ErrorDescription = "Details concerning the calling client application cannot be found in the database"
                });
            }

            // Flow the request_id to allow OpenIddict to restore
            // the original authorization request from the cache.

            //return this.View(new OpenIdDictAuthorizeViewModel
            //{
            //    ApplicationName = application.DisplayName,
            //    RequestId = request.RequestId,
            //    Scope = request.Scope
            //});

            return this.Ok();
        }

        [Authorize, HttpPost("~/connect/authorize/accept"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(OpenIdConnectRequest request)
        {
            // Retrieve the profile of the logged in user.
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.View("Error", new ErrorViewModel
                {
                    Error = OpenIdConnectConstants.Errors.ServerError,
                    ErrorDescription = "An internal error has occurred"
                });
            }

            // Create a new authentication ticket.
            var ticket = await this.CreateUserTicketAsync(request, user);

            // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
            return this.SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }

        [Authorize, HttpPost("~/connect/authorize/deny"), ValidateAntiForgeryToken]
        public IActionResult Deny()
        {
            // Notify OpenIddict that the authorization grant has been denied by the resource owner
            // to redirect the user agent to the client application using the appropriate response_mode.
            return this.Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
        }

        // Note: the logout action is only useful when implementing interactive
        // flows like the authorization code flow or the implicit flow.

        [HttpGet("~/connect/logout")]
        public IActionResult Logout(OpenIdConnectRequest request)
        {
            // Flow the request_id to allow OpenIddict to restore
            // the original logout request from the distributed cache.
            //return View(new LogoutViewModel
            //{
            //    RequestId = request.RequestId
            //});

            return this.Ok();
        }

        [HttpPost("~/connect/logout"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Ask ASP.NET Core Identity to delete the local and external cookies created
            // when the user agent is redirected from the external identity provider
            // after a successful authentication flow (e.g Google or Facebook).
            await this.signInManager.SignOutAsync();

            // Returning a SignOutResult will ask OpenIddict to redirect the user agent
            // to the post_logout_redirect_uri specified by the client application.
            return this.SignOut(OpenIdConnectServerDefaults.AuthenticationScheme);
        }

        // Note: to support non-interactive flows like password,
        // you must provide your own token endpoint action:

        [HttpPost("~/connect/token")]
        [Produces("application/json")]
        public async Task<IActionResult> Token(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                var user = await this.userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    return this.BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password couple is invalid."
                    });
                }

                // Ensure the user is allowed to sign in.
                if (!await this.signInManager.CanSignInAsync(user))
                {
                    return this.BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user is not allowed to sign in."
                    });
                }

                // Reject the token request if two-factor authentication has been enabled by the user.
                if (this.userManager.SupportsUserTwoFactor && await this.userManager.GetTwoFactorEnabledAsync(user))
                {
                    return this.BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user is not allowed to sign in."
                    });
                }

                // Ensure the user is not already locked out.
                if (this.userManager.SupportsUserLockout && await this.userManager.IsLockedOutAsync(user))
                {
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
                var ticket = await this.CreateUserTicketAsync(request, user);

                return this.SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }

            else if (request.IsClientCredentialsGrantType())
            {
                // Note: the client credentials are automatically validated by OpenIddict:
                // if client_id or client_secret are invalid, this action won't be invoked.

                var application = await this.applicationManager.FindByClientIdAsync(request.ClientId, this.HttpContext.RequestAborted);
                if (application == null)
                {
                    return this.BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidClient,
                        ErrorDescription = "The client application was not found in the database."
                    });
                }

                // Create a new authentication ticket.
                var ticket = this.CreateClientTicket(request, application);

                return this.SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }

            return this.BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant type is not supported."
            });
        }


        private AuthenticationTicket CreateClientTicket(OpenIdConnectRequest request, OpenIddictApplication application)
        {
            // Create a new ClaimsIdentity containing the claims that
            // will be used to create an id_token, a token or a code.
            var identity = new ClaimsIdentity(OpenIdConnectServerDefaults.AuthenticationScheme);

            // Use the client_id as the name identifier.
            identity.AddClaim(ClaimTypes.NameIdentifier, application.ClientId,
                OpenIdConnectConstants.Destinations.AccessToken,
                OpenIdConnectConstants.Destinations.IdentityToken);

            identity.AddClaim(ClaimTypes.Name, application.DisplayName,
                OpenIdConnectConstants.Destinations.AccessToken,
                OpenIdConnectConstants.Destinations.IdentityToken);

            // Create a new authentication ticket holding the user identity.
            return new AuthenticationTicket(
                new ClaimsPrincipal(identity),
                new AuthenticationProperties(),
                OpenIdConnectServerDefaults.AuthenticationScheme);
        }


        private async Task<AuthenticationTicket> CreateUserTicketAsync(OpenIdConnectRequest request, ApplicationUser user)
        {
            // Create a new ClaimsPrincipal containing the claims that
            // will be used to create an id_token, a token or a code.
            var principal = await this.signInManager.CreateUserPrincipalAsync(user);

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.

            foreach (var claim in principal.Claims)
            {
                // In this sample, every claim is serialized in both the access and the identity tokens.
                // In a real world application, you'd probably want to exclude confidential claims
                // or apply a claims policy based on the scopes requested by the client application.
                claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken,
                                      OpenIdConnectConstants.Destinations.IdentityToken);
            }

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(
                principal, new AuthenticationProperties(),
                OpenIdConnectServerDefaults.AuthenticationScheme);

            // Set the list of scopes granted to the client application.
            // Note: the offline_access scope must be granted
            // to allow OpenIddict to return a refresh token.
            ticket.SetScopes(new[] {
                OpenIdConnectConstants.Scopes.OpenId,
                OpenIdConnectConstants.Scopes.Email,
                OpenIdConnectConstants.Scopes.Profile,
                OpenIdConnectConstants.Scopes.OfflineAccess,
                OpenIddictConstants.Scopes.Roles
            }.Intersect(request.GetScopes()));

            return ticket;
        }
    }
}