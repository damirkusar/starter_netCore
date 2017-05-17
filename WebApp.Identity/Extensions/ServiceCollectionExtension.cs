using System;
using System.Net;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.DataAccessLayer;
using WebApp.DataAccessLayer.Models;

namespace WebApp.Identity.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                    options.UseOpenIddict();
                });

            services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromHours(24);
                o.Cookies.ExternalCookie.ExpireTimeSpan = TimeSpan.FromHours(24);
                o.Cookies.ApplicationCookie.LoginPath = "/login";
                o.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = ctx =>
                    {
                        // Needed so that API returns unauthorized instead of the default page with StatusCode.Ok
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                };
            })
                .AddEntityFrameworkStores<ApplicationDbContext, Guid>()
                .AddDefaultTokenProviders();

            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            var openIdDictBuilder = services.AddOpenIddict()
                // Register the Entity Framework stores.
                .AddEntityFrameworkCoreStores<ApplicationDbContext>()

                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                .AddMvcBinders()

                .EnableTokenEndpoint("/connect/token")

                .AllowPasswordFlow()
                .AllowClientCredentialsFlow()

                // During development, you can disable the HTTPS requirement.
                .DisableHttpsRequirement()

                 // Register a new ephemeral key, that is discarded when the application
                 // shuts down. Tokens signed using this key are automatically invalidated.
                 // This method should only be used during development.
                 .AddEphemeralSigningKey();

            openIdDictBuilder.SetAccessTokenLifetime(TimeSpan.FromHours(24));
            
            return services;
        }
    }
}