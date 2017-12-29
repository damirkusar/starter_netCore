using System.Threading.Tasks;
using Identity.Interface.TransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Identity.Interface.Services
{
    public interface IUserPasswordService
    {
        Task<IdentityResult> UpdateAsync(string userId, UserPassword userPassword);
    }
}