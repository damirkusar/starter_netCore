using System;
using System.Threading.Tasks;
using ApiGateway.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApiGateway.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            this.logger.LogError(1, exception, exception.Message);

            var apiInternalServerErrorResponse = new ApiInternalServerErrorResponse(exception.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = apiInternalServerErrorResponse.StatusCode;

            var result = JsonConvert.SerializeObject(apiInternalServerErrorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}