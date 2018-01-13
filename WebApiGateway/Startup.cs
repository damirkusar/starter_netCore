using System;
using AspNet.Security.OAuth.Introspection;
using Common.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Swashbuckle.AspNetCore.Swagger;
using WebApiGateway.Adaptor;
using WebApiGateway.Extensions;
using WebApiGateway.Settings;

namespace WebApiGateway
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = OAuthIntrospectionDefaults.AuthenticationScheme;
                })
                .AddOAuthIntrospection(options =>
                {
                    options.Authority = new Uri(this.Configuration["MicroserviceUrls:IdentityService"]);
                    options.Audiences.Add("web-api-gateway");
                    options.ClientId = "web-api-gateway";
                    options.ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342";
                    options.RequireHttpsMetadata = false;
                });

            // Configure api gateway
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICustomRestClient, CustomRestClient>();
            services.AddSingleton<IRestClient, RestClient>();
            // Configure AutoMapper for API Gateway
            services.ConfigureAutoMapper();

            // Add framework services.
            services.AddMvc();
            services.AddOptions();

            services.Configure<MicroserviceUrls>(this.Configuration.GetSection("MicroserviceUrls"));

            // Configure Swagger
            services.ConfigureSwaggerGen(options =>
                options.CustomSchemaIds(schemaId => schemaId.FullName)
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "angularXcore WebApiGateway API", Version = "v1"});
                c.AddSecurityDefinition("OpenIdDict", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "password",
                    TokenUrl = "http://localhost:4301/api/auth/token"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder =>
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .WithExposedHeaders("Content-Disposition", "Content-Type")
            );

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // Configure Middleware
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.UseMiddleware<GlobalTraceMiddleware>();

            app.UseMvcWithDefaultRoute();

            // Configure Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "angularXcore v1"); });
        }
    }
}