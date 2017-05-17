using Microsoft.AspNetCore.Builder;

namespace WebApp.Identity.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigureIdentity(this IApplicationBuilder app)
        {
            app.UseIdentity();
            app.UseOAuthValidation();
            app.UseOpenIddict();

            //app.UseGoogleAuthentication(new GoogleOptions()
            //{
            //    ClientId = Configuration["Authentication:Google:ClientId"],
            //    ClientSecret = Configuration["Authentication:Google:ClientSecret"]
            //});

            //app.UseFacebookAuthentication(new FacebookOptions()
            //{
            //    AppId = Configuration["Authentication:Facebook:AppId"],
            //    AppSecret = Configuration["Authentication:Facebook:AppSecret"]
            //});

            //app.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions()
            //{
            //    ClientId = Configuration["Authentication:Microsoft:ClientId"],
            //    ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"]
            //});

            return app;
        }
    }
}
