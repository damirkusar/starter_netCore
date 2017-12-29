using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvider.Profiles;
using ResourceTemplate.Profiles;

namespace ResourceProvider.Extensions
{
    public static class ServiceCollectionExtensionForAutoMapper
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(conf =>
            {
                conf.AddProfile<ResourceProviderProfile>();
                conf.AddProfile<ResourceProfile>();
            });

            return services;
        }
    }
}