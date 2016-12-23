using System;
using System.Web;
using System.Web.Mvc;

namespace Mvc5Application1.Infrastructure.Attribute
{
    /// <summary>
    /// Disable caching on browser
    /// </summary>
    public class DisableCacheAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            cache.SetCacheability(HttpCacheability.NoCache);
            cache.SetExpires(DateTime.Now);
            cache.SetAllowResponseInBrowserHistory(false);
            cache.SetNoServerCaching();
            cache.SetNoStore();
        }
    }
}