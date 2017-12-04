using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Interface
{
    public interface IChangeUserPasswordService
    {
        Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal user, string currentPassword, string newPassword);
        Task<IdentityResult> ForceChangePasswordAsync(string userName, string newPassword);
    }
}