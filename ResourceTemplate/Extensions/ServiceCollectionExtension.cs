using Microsoft.Extensions.DependencyInjection;
using ResourceTemplate.Data;
using ResourceTemplate.Interface.Services;
using ResourceTemplate.Services;

namespace ResourceTemplate.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureResource(this IServiceCollection services)
        {
            // Add Interface mappings
            services.AddScoped<IResourceTemplateDbContext, ResourceTemplateDbContext>();
            services.AddScoped<IResourceService, ResourceService>();

            return services;
        }
    }
}