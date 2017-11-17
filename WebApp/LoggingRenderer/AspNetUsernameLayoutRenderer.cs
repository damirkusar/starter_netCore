﻿using System.Text;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;
using WebApp.Identity.Extensions;

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
            var username = context.User.GetUsername();
            builder.Append(username);
        }
    }
}