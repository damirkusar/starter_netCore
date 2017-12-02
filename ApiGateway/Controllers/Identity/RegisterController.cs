using System.Net;
using System.Threading.Tasks;
using ApiGateway.Filters;
using ApiGateway.TransferObjects;
using AutoMapper;
using Identity.Interface;
using Identity.Interface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiGateway.Controllers.Identity
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly ILogger<ChangePasswordController> logger;
        private readonly IMapper mapper;
        private readonly IRegisterService registerService;

        public RegisterController(
            ILogger<ChangePasswordController> logger,
            IMapper mapper,
            IRegisterService registerService)
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