using System.Threading.Tasks;
using Identity.Interface.Data.Models;
using Identity.Interface.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services
{
    public class UpdateUserPassword: IUpdateUserPassword
    {
        private readonly ILogger<UpdateUserPassword> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public UpdateUserPassword(
            ILogger<UpdateUserPassword> logger,
            UserManager<ApplicationUser> userManager)
        {
            this.logger = logger;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> UpdateAsync(Interface.TransferObjects.ChangeUserPassword userPassword)
        {
            var applicationUser = await this.userManager.FindByIdAsync(userPassword.UserId);
            var result = await this.userManager.ChangePasswordAsync(applicationUser, userPassword.Password, userPassword.NewPassword);
            return result;
        }
    }
}