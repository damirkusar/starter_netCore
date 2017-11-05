using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Identity.Interface.Models;
using WebApp.TransferObjects;

namespace WebApp.Extensions
{
    public static class ServiceCollectionExtensionForAutoMapper
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(conf =>
            {
                conf.CreateMap<RegisterRequest, NewUser>().ReverseMap();
            });

            return services;
        }
    }
}