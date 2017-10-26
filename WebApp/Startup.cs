using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
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
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();

            env.ConfigureNLog("nlog.config");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<DataDbContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("DataConnection")));

            // Add identity services from WebApp.Identiy.
            services.ConfigureIdentity(this.Configuration);

            // Add framework services.
            services.AddMvc();
            services.AddOptions();
            services.AddNodeServices();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<DataLayer, DataLayer>();

            // Swagger
            services.ConfigureSwaggerGen(options =>
                options.CustomSchemaIds(schemaId => schemaId.FullName)
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "angularXcore API", Version = "v1"});
                c.AddSecurityDefinition("OpenIdDict", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "password",
                    TokenUrl = "/api/auth/token"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            app.AddNLogWeb();

            LogManager.Configuration.Variables["connectionString"] =
                this.Configuration.GetConnectionString("LogConnection");
            LogManager.Configuration.Variables["configDir"] = "C:\\temp\\";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure business layer
            app.ConfigureIdentity();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });

            app.UseCors(builder =>
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .WithExposedHeaders("Content-Disposition", "Content-Type")
            );

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "angularXcore v1"); });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

//            Initializer.CreateTestClient(app.ApplicationServices, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}