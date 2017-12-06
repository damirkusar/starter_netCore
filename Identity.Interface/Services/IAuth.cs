using System.Threading;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Identity.Interface.Services
{
    public interface IAuth
    {
        Task<bool> IsUserValidToSignInAsync(string userName, string password);
        Task<AuthenticationTicket> CreatePasswordGrantTypeTicketAsync(OpenIdConnectRequest request, IOptions<IdentityOptions> identityOptions);
        Task<bool> IsClientValidToSignInAsync(string clientId, CancellationToken cancellationToken);
        Task<AuthenticationTicket> CreateClientGrantTypeTicketAsync(OpenIdConnectRequest request, CancellationToken cancellationToken);
    }
}