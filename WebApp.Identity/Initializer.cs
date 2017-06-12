using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Core;
using OpenIddict.Models;
using WebApp.DataAccessLayer;

namespace WebApp.Identity
{
    public static class Initializer
    {
        public static async Task CreateTestClient(IServiceProvider services, CancellationToken cancellationToken)
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IdDbContext>();
                await context.Database.EnsureCreatedAsync(cancellationToken);

                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                if (await manager.FindByClientIdAsync("TestClient", cancellationToken) == null)
                {
                    var application = new OpenIddictApplication
                    {
                        ClientId = "TestClient",
                        DisplayName = "My TestClient application"
                    };

                    await manager.CreateAsync(application, "388D45FA-B36B-4988-BA59-B187D329C207", cancellationToken);
                }
            }
        }
    }
}
