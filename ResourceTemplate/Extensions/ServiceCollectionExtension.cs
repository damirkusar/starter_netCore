using System;
using AspNet.Security.OAuth.Introspection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ResourceTemplate.Data;
using ResourceTemplate.Interface.Services;
using ResourceTemplate.Services;

namespace ResourceTemplate.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureResource(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ResourceTemplateDbContext>(
                options =>
                {
                    options.UseSqlServer(connectionString);
                });

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = OAuthIntrospectionDefaults.AuthenticationScheme;
                })

                .AddOAuthIntrospection(options =>
                {
                    options.Authority = new Uri("http://localhost:4301/");
                    options.Audiences.Add("resource-server-1");
                    options.ClientId = "resource-server-1";
                    options.ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342";
                    options.RequireHttpsMetadata = false;
                });

            // Add Interface mappings
            services.AddScoped<IResourceTemplateDbContext, ResourceTemplateDbContext>();
            services.AddScoped<IResourceService, ResourceService>();

            return services;
        }
    }
}