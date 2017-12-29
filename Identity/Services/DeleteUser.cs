using System;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Data.Models;
using Identity.Interface.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services
{
    public class DeleteUser : IDeleteUser
    {
        private readonly ILogger<DeleteUser> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;


        public DeleteUser(
            ILogger<DeleteUser> logger,
            IMapper mapper,
            UserManager<ApplicationUser> userManager
            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task DeleteAsync(Guid userId)
        {
            var applicationUser = await this.userManager.FindByIdAsync(userId.ToString());
            await this.userManager.DeleteAsync(applicationUser);
        }
    }
}