using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Identity.Interface.Models;

namespace WebApp.Identity.Interface
{
    public interface IRegisterService
    {
        Task<IdentityResult> RegisterAsync(NewUser newUser);
    }
}