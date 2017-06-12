using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NuGet.Protocol.Core.v3;
using WebApp.DataAccessLayer.Models;
using WebApp.Identity.ViewModels.Account;

namespace WebApp.Identity.Controllers
{
    [AllowAnonymous]
    [Route("api/Account")]
    [ApiExplorerSettings(IgnoreApi = false)]
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

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterViewModel registerViewModel)
        {
            try
            {
                this.logger.Trace($"Register called with ViewModel: {registerViewModel.ToJson()}");
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var user = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    LockoutEnabled = true
                };

                IdentityResult result;
                if (registerViewModel.Password == null)
                {
                    user.GeneratedPassword = this.GeneratePassword();
                    result = this.userManager.CreateAsync(user, user.GeneratedPassword).Result;
                }
                else
                {
                    result = this.userManager.CreateAsync(user, registerViewModel.Password).Result;

                }

                if (!result.Succeeded)
                {
                    return this.GetErrorResult(result);
                }

                if (this.userManager.Users.Count() == 1)
                {
                    user = this.AssignFirstCreatedUserAsAdmin(user);
                }

                user = this.userManager.FindByEmailAsync(user.Email).Result;
                return this.Ok(user);
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in Register with ViewModel: {registerViewModel.ToJson()}");
                return this.BadRequest(exception);
            }
        }

        private string GeneratePassword()
        {
            return $"AnuglarXCore{new Random().Next(1111)}$";
        }

        private ApplicationUser AssignFirstCreatedUserAsAdmin(ApplicationUser user)
        {
            try
            {
                this.logger.Trace($"AssignFirstCreatedUserAsAdmin called with User: {user.ToJson()}");
                if (!this.roleManager.RoleExistsAsync("admin").Result)
                {
                    var role = new ApplicationRole()
                    {
                        Name = "admin"
                    };
                    var roleResult = this.roleManager.CreateAsync(role).Result;
                    if (!roleResult.Succeeded)
                    {
                        this.ModelState.AddModelError("", "Error while creating role!");
                        {
                            throw new InvalidOperationException($"Could not crate admin role");
                        }
                    }
                }

                var addUserToRoleResult = this.userManager.AddToRoleAsync(user, "admin").Result;
                if (!addUserToRoleResult.Succeeded)
                {
                    {
                        throw new InvalidOperationException($"Could not assign user {user} to admin role");
                    }
                }
                return (user);
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in AssignFirstCreatedUserAsAdmin with User: {user.ToJson()}");
                throw;
            }
        }

        [HttpPost]
        [Route("changePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordViewModel viewModel)
        {
            try
            {
                this.logger.Trace($"ChangePassword called with ViewModel: {viewModel.ToJson()}");
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
                this.logger.Error(exception, $"Error in ChangePassword with ViewModel: {viewModel.ToJson()}");
                return this.BadRequest(exception);
            }
        }

        [HttpPost]
        [Route("forceChangePassword")]
        public IActionResult ForceChangePassword([FromBody] ForceChangePasswordViewModel viewModel)
        {
            try
            {
                this.logger.Trace($"ForceChangePassword called with ViewModel: {viewModel.ToJson()}");
                if (this.ModelState.IsValid)
                {
                    var user = this.userManager.FindByEmailAsync(viewModel.Email).Result;
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
                this.logger.Error(exception, $"Error in ForceChangePassword with ViewModel: {viewModel.ToJson()}");
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