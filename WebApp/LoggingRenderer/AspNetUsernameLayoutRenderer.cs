using System.Text;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;

namespace WebApp.LoggingRenderer
{
    [LayoutRenderer("aspnet-user-name")]
    public class AspNetUsernameLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var context = this.HttpContextAccessor.HttpContext;

            if (context.User?.Identity == null)
            {
                return;
            }

            builder.Append(context.User.Identity.Name);
        }
    }
}