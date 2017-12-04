using AutoMapper;
using Identity.Interface.Models;
using Identity.Profiles;
using IdentityProvider.TransferObjects;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Extensions
{
    public static class ServiceCollectionExtensionForAutoMapper
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(conf =>
            {
                conf.AddProfile<IdentityProfile>();

                conf.CreateMap<RegisterUserRequest, RegisterUser>().ReverseMap();
                conf.CreateMap<RegisterClientRequest, RegisterClient>().ReverseMap();
            });

            return services;
        }
    }
}