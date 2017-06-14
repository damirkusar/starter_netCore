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
            services.AddDbContext<IdDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
                    options.UseOpenIddict();
                });

            services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Lockout.MaxFailedAccessAttempts = 5;
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = true;
                o.Password.RequiredLength = 6;
                o.Cookies.ExternalCookie.ExpireTimeSpan = TimeSpan.FromHours(24);
                o.Cookies.ApplicationCookie.AuthenticationScheme = "Cookie";
                o.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromHours(24);
                o.Cookies.ApplicationCookie.LoginPath = "/api/auth/login";
                o.Cookies.ApplicationCookie.LogoutPath = "/api/auth/logout";
                o.Cookies.ApplicationCookie.CookieName = "AngularXCore";
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
                .AddEntityFrameworkStores<IdDbContext, Guid>()
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
                .AddEntityFrameworkCoreStores<IdDbContext>()

                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                .AddMvcBinders()

                .EnableTokenEndpoint("/api/auth/token")

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