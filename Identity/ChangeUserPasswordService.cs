using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Interface;
using Identity.Interface.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity
{
    public class ChangeUserPasswordService: IChangeUserPasswordService
    {
        private readonly ILogger<ChangeUserPasswordService> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public ChangeUserPasswordService(
            ILogger<ChangeUserPasswordService> logger,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal user, string currentPassword, string newPassword)
        {
            var applicationUser = await this.userManager.GetUserAsync(user);
            var result = await this.userManager.ChangePasswordAsync(applicationUser, currentPassword, newPassword);
            return result;
        }

        public async Task<IdentityResult> ForceChangePasswordAsync(string userName, string newPassword)
        {
            var user = await this.userManager.FindByNameAsync(userName);
            var passwordToken = await this.userManager.GeneratePasswordResetTokenAsync(user);
            var result = await this.userManager.ResetPasswordAsync(user, passwordToken, newPassword);
            return result;
        }
    }
}