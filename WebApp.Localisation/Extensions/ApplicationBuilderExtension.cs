using Microsoft.AspNetCore.Builder;

namespace WebApp.Localisation.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigureLocalisation(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
