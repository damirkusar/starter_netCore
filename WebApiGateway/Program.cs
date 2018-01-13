using System;
using Common.LoggingRenderer;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Config;
using NLog.Web;

namespace WebApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("aspnet-user-id", typeof(AspNetUserIdLayoutRenderer));
            ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("aspnet-user-name", typeof(AspNetUsernameLayoutRenderer));

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Stopped program because of exception");
                throw;
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog()
                .Build();
    }
}
