using Identity.Data;
using Identity.Interface.Services;
using Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
           // Add Interface mappings
            services.AddScoped<IIdentityDbContext, IdentityDbContext>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserPasswordService, UserPasswordService>();
            services.AddScoped<IClientService, ClientService>();

            return services;
        }
    }
}