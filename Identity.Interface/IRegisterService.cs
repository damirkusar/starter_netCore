using System.Threading.Tasks;
using Identity.Interface.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Interface
{
    public interface IRegisterService
    {
        Task<IdentityResult> RegisterAsync(RegisterUser newUser);
    }
}