using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Interface.Services;
using Identity.Interface.TransferObjects;
using Microsoft.Extensions.Logging;
using OpenIddict.Core;
using OpenIddict.Models;

namespace Identity.Services
{
    public class ClientService : IClientService
    {
        private readonly ILogger<ClientService> logger;
        private readonly IMapper mapper;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager;

        public ClientService(
            ILogger<ClientService> logger,
            IMapper mapper,
            OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.openIddictApplicationManager = openIddictApplicationManager;
        }

        public async Task<Client> LoadAsync(string clientId)
        {
            var cancellationToken = CancellationToken.None;
            var application = await this.openIddictApplicationManager.FindByClientIdAsync(clientId, cancellationToken);

            if (application == null)
            {
                return null;
            }

            var client = new Client
            {
                ClientId = application.ClientId,
                DisplayName = application.DisplayName
            };
            return client;
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

        public async Task UpdateAsync(Client client)
        {
            var cancellationToken = CancellationToken.None;
            var application = await this.openIddictApplicationManager.FindByClientIdAsync(client.ClientId, cancellationToken);

            application.ClientId = client.ClientId;
            application.DisplayName = client.DisplayName;

            await this.openIddictApplicationManager.UpdateAsync(application, client.ClientSecret, cancellationToken);
        }

        public async Task DeleteAsync(string clientId)
        {
            var cancellationToken = CancellationToken.None;
            var application = await this.openIddictApplicationManager.FindByClientIdAsync(clientId, cancellationToken);
            await this.openIddictApplicationManager.DeleteAsync(application, cancellationToken);
        }
    }
}