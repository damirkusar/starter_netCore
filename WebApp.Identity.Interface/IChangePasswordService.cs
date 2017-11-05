using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Identity.Interface
{
    public interface IChangePasswordService
    {
        Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal user, string currentPassword, string newPassword);
        Task<IdentityResult> ForceChangePasswordAsync(string userName, string newPassword);
    }
}