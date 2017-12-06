using System.Threading.Tasks;
using Identity.Interface.TransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Identity.Interface.Services
{
    public interface IRegisterUser
    {
        Task<IdentityResult> RegisterAsync(RegisterUser user);
    }
}