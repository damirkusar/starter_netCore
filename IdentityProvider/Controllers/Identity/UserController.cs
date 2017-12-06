using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Extensions;
using Identity.Interface.Services;
using Identity.Interface.TransferObjects;
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
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly IMapper mapper;
        private readonly IRegisterUser registerService;
        private readonly IUpdateUserPassword updateUserPassword;

        public UserController(
            ILogger<UserController> logger,
            IMapper mapper,
            IRegisterUser registerService,
            IUpdateUserPassword updateUserPassword)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.registerService = registerService;
            this.updateUserPassword = updateUserPassword;
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

        [HttpPut("password")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await this.updateUserPassword.UpdateAsync(
                new ChangeUserPassword { UserId = this.User.GetUserId().ToString(), Password = request.CurrentPassword, NewPassword = request.NewPassword });
            if (!result.Succeeded)
            {
                return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
            }

            return this.NoContent();
        }
    }
}