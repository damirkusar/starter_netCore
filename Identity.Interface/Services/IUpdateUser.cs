using System.Threading.Tasks;
using Identity.Interface.TransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Identity.Interface.Services
{
    public interface IUpdateUser
    {
        Task<IdentityResult> UpdateAsync(UpdatedUser user);
    }
}