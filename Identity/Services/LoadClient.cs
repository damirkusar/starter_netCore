using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Data;
using Identity.Interface.Services;
using Identity.Interface.TransferObjects;
using Microsoft.Extensions.Logging;
using OpenIddict.Core;
using OpenIddict.Models;

namespace Identity.Services
{
    public class LoadClient : ILoadClient
    {
        private readonly ILogger<LoadClient> logger;
        private readonly IMapper mapper;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager;
        private readonly IIdentityDbContext identityDbContext;

        public LoadClient(
            ILogger<LoadClient> logger,
            IMapper mapper,
            OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager,
            IIdentityDbContext identityDbContext)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.openIddictApplicationManager = openIddictApplicationManager;
            this.identityDbContext = identityDbContext;
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
    }
}