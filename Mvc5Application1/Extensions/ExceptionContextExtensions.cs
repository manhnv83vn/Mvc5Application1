using System.Web.Mvc;

namespace Mvc5Application1.Extensions
{
    public static class ExceptionContextExtensions
    {
        public static void CreateViewResult(this ExceptionContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewData = filterContext.Controller.ViewData,
                TempData = filterContext.Controller.TempData
            };

            if (filterContext.Controller.ViewData["ViewName"] != null)
            {
                (filterContext.Result as ViewResult).ViewName =
                    filterContext.Controller.ViewData["ViewName"].ToString();
            }
        }
    }
}