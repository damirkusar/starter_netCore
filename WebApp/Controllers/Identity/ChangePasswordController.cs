using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.Attributes;
using WebApp.Filters;
using WebApp.Identity.Interface;
using WebApp.Identity.Interface.Constants;
using WebApp.TransferObjects;

namespace WebApp.Controllers.Identity
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class ChangePasswordController : Controller
    {
        private readonly ILogger<ChangePasswordController> logger;
        private readonly IMapper mapper;
        private readonly IChangePasswordService changePasswordService;


        public ChangePasswordController(
            ILogger<ChangePasswordController> logger,
            IMapper mapper,
            IChangePasswordService changePasswordService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.changePasswordService = changePasswordService;
        }

        [HttpPost]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IActionResult))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await this.changePasswordService.ChangePasswordAsync(this.User, request.CurrentPassword, request.NewPassword);
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
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IActionResult))]
        public async Task<IActionResult> ForceChangePassword([FromBody] ForceChangePasswordRequest request)
        {
            var result = await this.changePasswordService.ForceChangePasswordAsync(request.UserName, request.NewPassword);
            if (!result.Succeeded)
            {
                return this.StatusCode((int) HttpStatusCode.InternalServerError, result.Errors);
            }

            return this.NoContent();
        }
    }
}