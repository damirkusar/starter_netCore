using System.Threading.Tasks;
using AutoMapper;
using Identity.Data.Models;
using Identity.Interface.Services;
using Identity.Interface.TransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services
{
    public class UpdateUser : IUpdateUser
    {
        private readonly ILogger<UpdateUser> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public UpdateUser(
            ILogger<UpdateUser> logger,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> UpdateAsync(UpdatedUser user)
        {
            var applicationUser = await this.userManager.FindByIdAsync(user.UserId.ToString());
            applicationUser.FirstName = user.FirstName;
            applicationUser.LastName = user.LastName;
            applicationUser.Email = user.Email;

            var result = await this.userManager.UpdateAsync(applicationUser);
            return result;
        }
    }
}