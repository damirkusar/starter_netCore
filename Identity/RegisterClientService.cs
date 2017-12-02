using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Data;
using Identity.Interface;
using Identity.Interface.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict.Core;
using OpenIddict.Models;

namespace Identity
{
    public class RegisterClientService : IRegisterClientService
    {
        private readonly ILogger<RegisterClientService> logger;
        private readonly IMapper mapper;
        private readonly IServiceProvider services;

        public RegisterClientService(
            ILogger<RegisterClientService> logger,
            IMapper mapper,
            IServiceProvider services)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.services = services;
        }

        public async Task RegisterAsync(RegisterClient client)
        {
            var cancellationToken = CancellationToken.None;
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = this.services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
                await context.Database.EnsureCreatedAsync(cancellationToken);

                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                if (await manager.FindByClientIdAsync(client.ClientId, cancellationToken) == null)
                {
                    var application = new OpenIddictApplication
                    {
                        ClientId = client.ClientId,
                        DisplayName = client.DisplayName
                    };

                    var openIddictApplication = await manager.CreateAsync(application, client.ClientSecret, cancellationToken);
                }
            }
        }
    }
}