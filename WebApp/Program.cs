using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog.Web;

namespace WebApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // NLog: setup the logger first to catch all errors
            //var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                //logger.Debug("init main");

                var config = new ConfigurationBuilder()
                    .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                    .AddCommandLine(args)
                    .Build();

                var host = new WebHostBuilder()
                    //.ConfigureLogging(options => options.AddConsole())
                    //.ConfigureLogging(options => options.AddDebug())
                    //.UseNLog()
                    .UseConfiguration(config)
                    .UseIISIntegration()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>()
                    .Build();

                host.Run();
            }
            catch (Exception e)
            {
                //NLog: catch setup errors
                //logger.Error(e, "Stopped program because of exception");
                throw;
            }
        }
    }
}
