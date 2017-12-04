using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Interface;
using Identity.Interface.Models;
using IdentityProvider.Filters;
using IdentityProvider.TransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityProvider.Controllers.Identity
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class RegisterUserController : Controller
    {
        private readonly ILogger<RegisterUserController> logger;
        private readonly IMapper mapper;
        private readonly IRegisterUserService registerService;

        public RegisterUserController(
            ILogger<RegisterUserController> logger,
            IMapper mapper,
            IRegisterUserService registerService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.registerService = registerService;
        }

        [HttpPost]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var newUser = this.mapper.Map<RegisterUserRequest, RegisterUser>(request);
            var result = await this.registerService.RegisterAsync(newUser);
            if (!result.Succeeded)
            {
                return this.StatusCode((int) HttpStatusCode.InternalServerError, result.Errors);
            }

            return this.NoContent();
        }
    }
}