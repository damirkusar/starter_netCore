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
    public class LoadUser : ILoadUser
    {
        private readonly ILogger<LoadUser> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public LoadUser(
            ILogger<LoadUser> logger,
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
    }
}