using System.Text;
using Identity.Extensions;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;

namespace IdentityProvider.LoggingRenderer
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
            var username = context.User.GetUserName();
            builder.Append(username);
        }
    }
}