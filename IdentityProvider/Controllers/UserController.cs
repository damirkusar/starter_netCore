using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Extensions;
using Common.Filters;
using Identity.Interface.Services;
using Identity.Interface.TransferObjects;
using IdentityProvider.TransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityProvider.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IUserPasswordService updateUserPassword;

        public UserController(
            ILogger<UserController> logger,
            IMapper mapper,
            IUserService userService,
            IUserPasswordService updateUserPassword)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userService = userService;
            this.updateUserPassword = updateUserPassword;
        }

        [HttpGet]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IList<User>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> GetUsers()
        {
            var users = await this.userService.LoadAsync();

            return this.Ok(users);
        }

        [HttpGet("{userId}")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(User))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await this.userService.LoadAsync(userId);

            return this.Ok(user);
        }

        [HttpPut("{userId}")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(User))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequest request)
        {
            var updatedUser = this.mapper.Map<UpdateUserRequest, UpdatedUser>(request);
            updatedUser.UserId = userId;
            var result = await this.userService.UpdateAsync(updatedUser);
            if (!result.Succeeded)
            {
                return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
            }

            return this.Ok(request);
        }

        [HttpDelete("{userId}")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            await this.userService.DeleteAsync(userId);

            return this.NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var newUser = this.mapper.Map<RegisterUserRequest, RegisterUser>(request);
            var result = await this.userService.RegisterAsync(newUser);
            if (!result.Succeeded)
            {
                return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
            }

            return this.NoContent();
        }

        [AllowAnonymous]
        [HttpPut("password")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> ChangePassword([FromBody] UpdateUserPasswordRequest request)
        {
            var result = await this.updateUserPassword.UpdateAsync(this.User.GetUserId().ToString(),
                new UserPassword { Password = request.CurrentPassword, NewPassword = request.NewPassword });
            if (!result.Succeeded)
            {
                return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
            }

            return this.NoContent();
        }
    }
}