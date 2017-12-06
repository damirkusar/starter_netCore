using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Interface.Constants;
using Identity.Interface.Services;
using IdentityProvider.Attributes;
using IdentityProvider.Filters;
using IdentityProvider.TransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityProvider.Controllers.Identity
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class PasswordController : Controller
    {
        private readonly ILogger<PasswordController> logger;
        private readonly IMapper mapper;
        private readonly IChangeUserPassword changeUserPasswordService;


        public PasswordController(
            ILogger<PasswordController> logger,
            IMapper mapper,
            IChangeUserPassword changeUserPasswordService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.changeUserPasswordService = changeUserPasswordService;
        }

        [HttpPost]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await this.changeUserPasswordService.ChangePasswordAsync(this.User, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return this.StatusCode((int) HttpStatusCode.InternalServerError, result.Errors);
            }

            return this.NoContent();
        }

        [AuthorizeRoles(Roles.Admin)]
        [HttpPost]
        [Route("force")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> ForceChangePassword([FromBody] ForceChangePasswordRequest request)
        {
            var result = await this.changeUserPasswordService.ForceChangePasswordAsync(request.UserName, request.NewPassword);
            if (!result.Succeeded)
            {
                return this.StatusCode((int) HttpStatusCode.InternalServerError, result.Errors);
            }

            return this.NoContent();
        }
    }
}