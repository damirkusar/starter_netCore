using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Localisation.DataAccessLayer;
using WebApp.Localisation.Interface;

namespace WebApp.Localisation.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureLocalisation(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LocalisationDbContext>(
                options =>
                {
                    options.UseSqlServer(connectionString);
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