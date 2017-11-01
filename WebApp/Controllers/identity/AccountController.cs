using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Filters;
using WebApp.Identity.DataAccessLayer.Models;
using WebApp.Identity.ViewModels.Account;

namespace WebApp.Controllers.identity
{
    [Authorize]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(
            ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager)
        {
            this.logger = logger;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        [ValidateModelState]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            var dbUser = await this.userManager.FindByEmailAsync(registerViewModel.Email);
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

            var result = await this.userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                return this.StatusCode(500, "Could not create user");
            }

            user = await this.userManager.FindByNameAsync(user.UserName);
            return this.Ok(user);
        }


        [HttpPost]
        [Route("changePassword")]
        [ValidateModelState]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel viewModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var result = await this.userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);

            if (result.Succeeded)
            {
                return this.NoContent();
            }

            return this.StatusCode(500, "Could not change password");
        }

        [HttpPost]
        [Route("forceChangePassword")]
        [ValidateModelState]
        public async Task<IActionResult> ForceChangePassword([FromBody] ForceChangePasswordViewModel viewModel)
        {
            var user = await this.userManager.FindByNameAsync(viewModel.UserName);
            var passwordToken =  await this.userManager.GeneratePasswordResetTokenAsync(user);
            var result = await this.userManager.ResetPasswordAsync(user, passwordToken, viewModel.NewPassword);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.StatusCode(500, "Could not change password");
        }
    }
}