using System.Threading.Tasks;
using Identity.Interface.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Interface.Services
{
    public interface IRegisterUserService
    {
        Task<IdentityResult> RegisterAsync(RegisterUser user);
    }
}