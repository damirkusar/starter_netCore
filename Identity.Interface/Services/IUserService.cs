using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Interface.TransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Identity.Interface.Services
{
    public interface IUserService
    {
        Task<IList<User>> LoadAsync();
        Task<User> LoadAsync(Guid userId);
        Task<IdentityResult> RegisterAsync(RegisterUser user);
        Task<IdentityResult> UpdateAsync(UpdatedUser user);
        Task DeleteAsync(Guid userId);

    }
}