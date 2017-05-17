using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using OpenIddict.Core;
using OpenIddict.Models;
using Swashbuckle.AspNetCore.Swagger;
using WebApp.DataAccessLayer;
using WebApp.Identity.Extensions;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<DataDbContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultDataConnection")));

            // Add identity services from WebApp.Identiy.
            services.ConfigureIdentity(this.Configuration);

            // Add framework services.
            services.AddMvc();
            services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("WebApp.Identity")));
            services.AddOptions();
            services.AddNodeServices();

            services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc("v1", new Info { Title = "angularXcore API", Version = "v1" });
              c.AddSecurityDefinition("OpenIdDict", new OAuth2Scheme
              {
                  Type = "oauth2",
                  Flow = "password",
                  TokenUrl = "/connect/token"
              });
          });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<DataLayer, DataLayer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            app.AddNLogWeb();
            env.ConfigureNLog("nlog.config");
            LogManager.Configuration.Variables["connectionString"] = this.Configuration.GetConnectionString("DefaultLogConnection");
            LogManager.Configuration.Variables["configDir"] = "C:\\temp\\";
            var logger = LogManager.GetCurrentClassLogger();

            if (env.IsDevelopment())
            {
                logger.Trace($"Environment in WebApp isDevelopment: {env.EnvironmentName}");
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                logger.Trace($"Environment in WebApp !isDevelopment: {env.EnvironmentName}");
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Add identity services from WebApp.Identiy.
            app.ConfigureIdentity();

            app.UseCors(builder =>
                builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
            );

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "angularXcore v1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            this.InitializeAsync(app.ApplicationServices, CancellationToken.None).GetAwaiter().GetResult();
        }

        private async Task InitializeAsync(IServiceProvider services, CancellationToken cancellationToken)
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.EnsureCreatedAsync(cancellationToken);

                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                if (await manager.FindByClientIdAsync("TestClient", cancellationToken) == null)
                {
                    var application = new OpenIddictApplication
                    {
                        ClientId = "TestClient",
                        DisplayName = "My TestClient application"
                    };

                    await manager.CreateAsync(application, "388D45FA-B36B-4988-BA59-B187D329C207", cancellationToken);
                }
            }
        }
    }
}