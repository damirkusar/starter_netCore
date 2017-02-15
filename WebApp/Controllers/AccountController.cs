using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
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

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> loginManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        [Route("UserInfo")]
        public IActionResult GetUserInfo()
        {
            var userInfo = this.CreateUserInfo();
            return this.Ok(userInfo);
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterViewModel registerViewModel)
        {
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

        private IActionResult AssignFirstCreatedUserAsAdmin(ApplicationUser user)
        {
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

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.loginManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false).Result;

                if (result.Succeeded)
                {
                    return this.Ok(loginViewModel);
                }

                this.ModelState.AddModelError("", "Invalid login!");
            }

            return this.BadRequest(loginViewModel);
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            this.loginManager.SignOutAsync().Wait();
            return this.Ok();
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
            var user = this.userManager.GetUserAsync(this.User).Result;
            var userInfo = new UserInfoViewModel(user)
            {
                AssignedRoles = this.userManager.GetRolesAsync(user).Result
            };

            return userInfo;
        }
    }
}