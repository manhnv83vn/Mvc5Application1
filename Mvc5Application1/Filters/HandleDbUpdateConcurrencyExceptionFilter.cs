using Mvc5Application1.Extensions;
using Mvc5Application1.Framework.Logging;
using System;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;

namespace Mvc5Application1.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class HandleDbUpdateConcurrencyExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private readonly ILogger logger;

        public HandleDbUpdateConcurrencyExceptionFilter(ILogger logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is DbUpdateConcurrencyException)
            {
                filterContext.Controller.ViewData.ModelState.AddModelError(string.Empty, "Unable to save changes. The entity was deleted or updated by another user.");

                filterContext.CreateViewResult();

                filterContext.ExceptionHandled = true;

                logger.LogInformational(filterContext.Exception.Message);
            }
        }
    }
}