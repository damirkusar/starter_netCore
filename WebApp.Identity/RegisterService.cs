using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using WebApp.Identity.DataAccessLayer.Models;
using WebApp.Identity.Interface;
using WebApp.Identity.Interface.Models;

namespace WebApp.Identity
{
    public class RegisterService: IRegisterService
    {
        private readonly ILogger<RegisterService> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public RegisterService(
            ILogger<RegisterService> logger,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUser newUser)
        {
            var newApplicationUser = this.mapper.Map<RegisterUser, ApplicationUser>(newUser);
            var result = await this.userManager.CreateAsync(newApplicationUser, newUser.Password);
            return result;
        }
    }
}