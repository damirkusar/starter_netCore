using System;
using System.Threading;
using System.Threading.Tasks;
using Identity.Interface.Services;
using Identity.Interface.TransferObjects;
using Microsoft.Extensions.Logging;
using OpenIddict.Core;
using OpenIddict.Models;

namespace Identity.Services
{
    public class UpdateClient : IUpdateClient
    {
        private readonly ILogger<UpdateClient> logger;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager;

        public UpdateClient(
            ILogger<UpdateClient> logger,
            OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager)
        {
            this.logger = logger;
            this.openIddictApplicationManager = openIddictApplicationManager;
        }

        public async Task UpdateAsync(Client client)
        {
            var cancellationToken = CancellationToken.None;
            var application = await this.openIddictApplicationManager.FindByClientIdAsync(client.ClientId, cancellationToken);

            application.ClientId = client.ClientId;
            application.DisplayName = client.DisplayName;
      
            await this.openIddictApplicationManager.UpdateAsync(application, client.ClientSecret, cancellationToken);
        }
    }
}