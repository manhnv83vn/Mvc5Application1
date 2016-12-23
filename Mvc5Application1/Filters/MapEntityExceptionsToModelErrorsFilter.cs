using Mvc5Application1.Extensions;
using Mvc5Application1.Framework.Logging;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace Mvc5Application1.Filters
{
    public class MapEntityExceptionsToModelErrorsFilter : FilterAttribute, IExceptionFilter
    {
        private readonly ILogger logger;

        public MapEntityExceptionsToModelErrorsFilter(ILogger logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is DbEntityValidationException)
            {
                foreach (var error in ((DbEntityValidationException)filterContext.Exception).EntityValidationErrors.First().ValidationErrors)
                {
                    if (string.IsNullOrEmpty(error.PropertyName))
                    {
                        filterContext.Controller.ViewData.ModelState.AddModelError("", error.ErrorMessage);
                    }
                    else if (filterContext.Controller.ViewData.ModelState.ContainsKey(error.PropertyName) && filterContext.Controller.ViewData.ModelState[error.PropertyName].Errors.Count == 0)
                    {
                        filterContext.Controller.ViewData.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    else
                    {
                        bool found = false;
                        if (filterContext.Controller.ViewData.ModelState.ContainsKey(error.PropertyName))
                        {
                            found = filterContext.Controller.ViewData.ModelState[error.PropertyName].Errors.Any(errorItem => errorItem.ErrorMessage == error.ErrorMessage);
                        }
                        if (!found)
                        {
                            filterContext.Controller.ViewData.ModelState.AddModelError("", error.ErrorMessage);
                        }
                    }
                }

                filterContext.CreateViewResult();

                filterContext.ExceptionHandled = true;

                logger.LogInformational(filterContext.Exception.Message);
            }
        }
    }
}