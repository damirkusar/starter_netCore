using System;
using System.Text;
using Common.Extensions;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;

namespace Common.LoggingRenderer
{
    [LayoutRenderer("aspnet-user-id")]
    public class AspNetUserIdLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var context = this.HttpContextAccessor.HttpContext;

            if (context.User?.Identity == null)
            {
                return;
            }

            var userId = context.User.GetUserId();
            if (!userId.Equals(Guid.Empty))
            {
                builder.Append(userId);
            }
            else
            {
                builder.Append(string.Empty);
            }

        }
    }
}