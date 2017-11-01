using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Localisation.DataAccessLayer;
using WebApp.Localisation.Interface;

namespace WebApp.Localisation.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureLocalisation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LocalisationDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("LocalisationConnection"));
                });

            services.AddAutoMapper(conf =>
            {
            });

            services.AddScoped<ILocalisationDbContext, LocalisationDbContext>();
            services.AddScoped<ILocalisationService, LocalisationService>();

            return services;
        }
    }
}