using Microsoft.AspNetCore.Builder;

namespace WebApp.Identity.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigureIdentity(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            return app;
        }
    }
}
