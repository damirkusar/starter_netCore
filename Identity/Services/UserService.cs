using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Data.Models;
using Identity.Interface.Services;
using Identity.Interface.TransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(
            ILogger<UserService> logger,
            IMapper mapper,
            UserManager<ApplicationUser> userManager
            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IList<User>> LoadAsync()
        {
            var applicationUser = await this.userManager.Users.ToListAsync();
            var users = this.mapper.Map<IList<ApplicationUser>, IList<User>>(applicationUser);
            return users;
        }

        public async Task<User> LoadAsync(Guid userId)
        {
            var applicationUser = await this.userManager.FindByIdAsync(userId.ToString());
            var user = this.mapper.Map<ApplicationUser, User>(applicationUser);
            return user;
        }

        public async Task<IdentityResult> RegisterAsync(Interface.TransferObjects.RegisterUser user)
        {
            var newApplicationUser = this.mapper.Map<Interface.TransferObjects.RegisterUser, ApplicationUser>(user);
            var result = await this.userManager.CreateAsync(newApplicationUser, user.Password);
            return result;
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

        public async Task DeleteAsync(Guid userId)
        {
            var applicationUser = await this.userManager.FindByIdAsync(userId.ToString());
            await this.userManager.DeleteAsync(applicationUser);
        }
    }
}