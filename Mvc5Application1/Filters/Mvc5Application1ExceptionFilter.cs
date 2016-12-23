using Mvc5Application1.Business.Contracts.Exceptions;
using Mvc5Application1.Extensions;
using Mvc5Application1.Framework.Logging;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc5Application1.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class Mvc5Application1ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private readonly ILogger _logger;

        public Mvc5Application1ExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is Mvc5Application1Exception)
            {
                var exception = (Mvc5Application1Exception)filterContext.Exception;
                string fieldName = exception.FieldName ?? string.Empty;

                filterContext.Controller.ViewData.ModelState.AddModelError(fieldName, exception.Message);
                filterContext.CreateViewResult();
                filterContext.ExceptionHandled = true;

                _logger.LogInformational(filterContext.Exception.Message);
            }

            if (!filterContext.ExceptionHandled && filterContext.Exception is ApplicationException)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new RedirectResult("~/Home/PermissionError");
                    filterContext.ExceptionHandled = true;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                                    {
                                        { "action", "SignOut" },
                                        { "controller", "Auth" },
                                        { "area", ""}
                                    });

                    filterContext.ExceptionHandled = true;
                }
            }
        }
    }
}