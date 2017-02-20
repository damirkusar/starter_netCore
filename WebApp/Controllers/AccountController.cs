using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NuGet.Protocol.Core.v3;
using WebApp.DataAccessLayer.Models;
using WebApp.ViewModels.Account;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> loginManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly Logger logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> loginManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
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

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
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
                    LastName = registerViewModel.LastName
                };

                var result = this.userManager.CreateAsync(user, registerViewModel.Password).Result;
                if (!result.Succeeded)
                {
                    return this.GetErrorResult(result);
                }

                if (this.userManager.Users.Count() == 1)
                {
                    return this.AssignFirstCreatedUserAsAdmin(user);
                }

                return this.Ok(user);
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in Register with ViewModel: {registerViewModel.ToJson()}");
                return this.BadRequest(exception);
            }
        }

        private IActionResult AssignFirstCreatedUserAsAdmin(ApplicationUser user)
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
                            return this.StatusCode(500, this.ModelState);
                        }
                    }
                }

                var addUserToRoleResult = this.userManager.AddToRoleAsync(user, "admin").Result;
                if (!addUserToRoleResult.Succeeded)
                {
                    {
                        return this.GetErrorResult(addUserToRoleResult);
                    }
                }
                return this.Ok(user);
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in AssignFirstCreatedUserAsAdmin with User: {user.ToJson()}");
                return this.BadRequest(exception);
            }
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                this.logger.Trace($"Login called with ViewModel: {loginViewModel.ToJson()}");
                if (this.ModelState.IsValid)
                {
                    var result =
                        this.loginManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password,
                            loginViewModel.RememberMe, false).Result;

                    if (result.Succeeded)
                    {
                        return this.Ok(loginViewModel);
                    }

                    this.ModelState.AddModelError("", "Invalid login!");
                }

                return this.BadRequest(loginViewModel);
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in Login with ViewModel: {loginViewModel.ToJson()}");
                return this.BadRequest(exception);
            }
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            try
            {
                this.logger.Trace($"Logout called");
                this.loginManager.SignOutAsync().Wait();
                return this.Ok();
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in Logout");
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

        private UserInfoViewModel CreateUserInfo()
        {
            try
            {
                this.logger.Trace($"CreateUserInfo called");
                var user = this.userManager.GetUserAsync(this.User).Result;
                var userInfo = new UserInfoViewModel(user)
                {
                    AssignedRoles = this.userManager.GetRolesAsync(user).Result
                };

                return userInfo;
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in CreateUserInfo");
                throw;
            }
        }
    }
}