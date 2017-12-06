using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Interface.Services;
using Microsoft.Extensions.Logging;
using OpenIddict.Core;
using OpenIddict.Models;

namespace Identity.Services
{
    public class RegisterClient : IRegisterClient
    {
        private readonly ILogger<RegisterClient> logger;
        private readonly IMapper mapper;
        private readonly IServiceProvider services;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager;

        public RegisterClient(
            ILogger<RegisterClient> logger,
            IMapper mapper,
            IServiceProvider services,
            OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.services = services;
            this.openIddictApplicationManager = openIddictApplicationManager;
        }

        public async Task RegisterAsync(Interface.TransferObjects.RegisterClient client)
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

                var openIddictApplication = await this.openIddictApplicationManager.CreateAsync(application, cancellationToken);
            }
        }
    }
}