using System.Threading.Tasks;
using AutoMapper;
using Identity.Data.Models;
using Identity.Interface.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services
{
    public class RegisterUser: IRegisterUser
    {
        private readonly ILogger<RegisterUser> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public RegisterUser(
            ILogger<RegisterUser> logger,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(Interface.TransferObjects.RegisterUser user)
        {
            var newApplicationUser = this.mapper.Map<Interface.TransferObjects.RegisterUser, ApplicationUser>(user);
            var result = await this.userManager.CreateAsync(newApplicationUser, user.Password);
            return result;
        }
    }
}