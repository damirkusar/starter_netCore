using Microsoft.AspNetCore.Builder;

namespace ResourceTemplate.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigureResource(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            return app;
        }
    }
}
