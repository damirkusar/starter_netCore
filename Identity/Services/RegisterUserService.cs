using System.Threading.Tasks;
using AutoMapper;
using Identity.Interface.Data.Models;
using Identity.Interface.Models;
using Identity.Interface.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services
{
    public class RegisterUserService: IRegisterUserService
    {
        private readonly ILogger<RegisterUserService> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public RegisterUserService(
            ILogger<RegisterUserService> logger,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUser user)
        {
            var newApplicationUser = this.mapper.Map<RegisterUser, ApplicationUser>(user);
            var result = await this.userManager.CreateAsync(newApplicationUser, user.Password);
            return result;
        }
    }
}