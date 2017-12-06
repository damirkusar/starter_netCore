using AutoMapper;
using Identity.Profiles;
using IdentityProvider.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Extensions
{
    public static class ServiceCollectionExtensionForAutoMapper
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(conf =>
            {
                conf.AddProfile<IdentityProviderProfile>();
                conf.AddProfile<IdentityProfile>();
            });

            return services;
        }
    }
}