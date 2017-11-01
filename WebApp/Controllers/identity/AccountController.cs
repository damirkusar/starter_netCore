using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using WebApp.Identity.DataAccessLayer.Models;
using WebApp.Identity.ViewModels.Account;

namespace WebApp.Controllers.identity
{
    [Authorize]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly Logger logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterViewModel registerViewModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var dbUser = this.userManager.FindByEmailAsync(registerViewModel.Email).Result;
                if (dbUser != null)
                {
                    return this.BadRequest("Email is already taken");
                }

                var user = new ApplicationUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    LockoutEnabled = true
                };

                var result = this.userManager.CreateAsync(user, registerViewModel.Password).Result;
                if (!result.Succeeded)
                {
                    return this.GetErrorResult(result);
                }

                user = this.userManager.FindByNameAsync(user.UserName).Result;
                return this.Ok(user);
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception);
            }
        }


        [HttpPost]
        [Route("changePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordViewModel viewModel)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var user = this.userManager.GetUserAsync(this.User).Result;
                    var result = this.userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword).Result;

                    if (result.Succeeded)
                    {
                        return this.Ok();
                    }

                    this.ModelState.AddModelError("", "Error in changing password!");
                }

                return this.BadRequest();
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception);
            }
        }

        [HttpPost]
        [Route("forceChangePassword")]
        public IActionResult ForceChangePassword([FromBody] ForceChangePasswordViewModel viewModel)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var user = this.userManager.FindByNameAsync(viewModel.UserName).Result;
                    var passwordToken = this.userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var result = this.userManager.ResetPasswordAsync(user, passwordToken, viewModel.NewPassword).Result;

                    if (result.Succeeded)
                    {
                        return this.Ok();
                    }

                    this.ModelState.AddModelError("", "Error in changing password!");
                }

                return this.BadRequest();
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception);
            }
        }

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.StatusCode(500, "Could not create User");
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        this.ModelState.AddModelError(error.Code, error.Description);
                    }
                }

                if (this.ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return this.BadRequest();
                }

                return this.BadRequest(this.ModelState);
            }

            return null;
        }
    }
}