using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Localisation.DataAccessLayer;
using WebApp.Localisation.Interface;
using WebApp.Localisation.Profiles;

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
                conf.AddProfile<LocalisationProfile>();
            });

            services.AddScoped<ILocalisationDbContext, LocalisationDbContext>();
            services.AddScoped<ILocalisationService, LocalisationService>();

            return services;
        }
    }
}