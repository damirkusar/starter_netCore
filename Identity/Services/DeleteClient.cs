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
    public class DeleteClient : IDeleteClient
    {
        private readonly ILogger<DeleteClient> logger;
        private readonly IMapper mapper;
        private readonly IServiceProvider services;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager;

        public DeleteClient(
            ILogger<DeleteClient> logger,
            IMapper mapper,
            IServiceProvider services,
            OpenIddictApplicationManager<OpenIddictApplication> openIddictApplicationManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.services = services;
            this.openIddictApplicationManager = openIddictApplicationManager;
        }

        public async Task DeleteAsync(string clientId)
        {
            var cancellationToken = CancellationToken.None;
            var application = await this.openIddictApplicationManager.FindByClientIdAsync(clientId, cancellationToken);
            await this.openIddictApplicationManager.DeleteAsync(application, cancellationToken);
        }
    }
}