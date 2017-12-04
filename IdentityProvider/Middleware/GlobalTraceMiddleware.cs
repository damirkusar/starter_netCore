using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IdentityProvider.Middleware
{
    public class GlobalTraceMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalTraceMiddleware> logger;

        public GlobalTraceMiddleware(RequestDelegate next, ILogger<GlobalTraceMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                this.logger.LogTrace("");
            }
            await this.next(context);
        }
    }
}