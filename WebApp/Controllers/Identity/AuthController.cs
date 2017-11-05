using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.Identity.Interface;
using SignInResult = Microsoft.AspNetCore.Mvc.SignInResult;

namespace WebApp.Controllers.Identity
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> logger;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly IOptions<IdentityOptions> identityOptions;

        public AuthController(
            ILogger<AuthController> logger,
            IMapper mapper,
            IAuthService authService,
            IOptions<IdentityOptions> identityOptions)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.authService = authService;
            this.identityOptions = identityOptions;
        }

        [HttpPost]
        [Route("token")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(SignInResult))]
        public async Task<IActionResult> Token([FromBody] OpenIdConnectRequest request)
        {
            Debug.Assert(request.IsTokenRequest(), 
                "The OpenIddict binder for ASP.NET Core MVC is not registered. " + 
                "Make sure services.AddOpenIddict().AddMvcBinders() is correctly called.");

            if (request.IsPasswordGrantType())
            {
                return await this.HandlePasswordGrantType(request);
            }

            this.logger.LogError($"Error in Token: The specified grant ({request.GrantType}) type is not supported");

            return this.BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant type is not supported."
            });
        }

        private async Task<IActionResult> HandlePasswordGrantType(OpenIdConnectRequest request)
        {
            var valid = await this.authService.IsUserValidToSignInAsync(request.Username, request.Password);
            if (!valid)
            {
                return this.BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "The username/password couple is invalid."
                });
            }

            // Create a new authentication ticket.
            var ticket = await this.authService.CreatePasswordGrantTypeTicketAsync(request, this.identityOptions);

            return this.SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }
    }
}