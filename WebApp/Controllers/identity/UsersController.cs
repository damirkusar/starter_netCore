using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using WebApp.DataAccessLayer.Models;

namespace WebApp.Controllers.identity
{
    [Authorize]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly Logger logger;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        [HttpGet]
        [Route("UserInfo")]
        public IActionResult GetUserInfo()
        {
            try
            {
                this.logger.Trace($"GetUserInfo called");
                var userInfo = this.CreateUserInfo();
                return this.Ok(userInfo);
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in GetUserInfo");
                return this.BadRequest(exception);
            }
        }

        private ApplicationUser CreateUserInfo()
        {
            try
            {
                this.logger.Trace($"CreateUserInfo called");
                var user = this.userManager.GetUserAsync(this.User).Result;

                return user;
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in CreateUserInfo");
                throw;
            }
        }
    }
}