using System.Threading;
using System.Threading.Tasks;
using Identity.Interface.Services;
using Identity.Interface.TransferObjects;
using Microsoft.Extensions.Logging;
using OpenIddict.Core;
using OpenIddict.Models;

namespace Identity.Services
{
    public class RegisterClient : IRegisterClient
    {
        private readonly ILogger<RegisterClient> logger;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager;

        public RegisterClient(
            ILogger<RegisterClient> logger,
            OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager)
        {
            this.logger = logger;
            this.openIddictApplicationManager = openIddictApplicationManager;
        }

        public async Task RegisterAsync(Client client)
        {
            var cancellationToken = CancellationToken.None;

            if (await this.openIddictApplicationManager.FindByClientIdAsync(client.ClientId, cancellationToken) == null)
            {
                var application = new OpenIddictApplicationDescriptor
                {
                    ClientId = client.ClientId,
                    ClientSecret = client.ClientSecret,
                    DisplayName = client.DisplayName
                };

                await this.openIddictApplicationManager.CreateAsync(application, cancellationToken);
            }
        }
    }
}