using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WebApiGateway.Profiles;

namespace WebApiGateway.Extensions
{
    public static class ServiceCollectionExtensionForAutoMapper
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(conf =>
            {
                conf.AddProfile<WebApiGatewayProfile>();
            });

            return services;
        }
    }
}