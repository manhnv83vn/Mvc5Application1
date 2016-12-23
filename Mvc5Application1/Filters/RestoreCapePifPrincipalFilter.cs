using Mvc5Application1.Framework.Security;
using System.Threading;
using System.Web.Mvc;

namespace Mvc5Application1.Filters
{
    public class RestoreMvc5Application1PrincipalFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext != null && filterContext.HttpContext.Session != null)
            {
                var principal = filterContext.HttpContext.Session["Principal"];
                if (principal != null)
                {
                    Thread.CurrentPrincipal = (Mvc5Application1Principal)principal;
                }
            }
        }
    }
}