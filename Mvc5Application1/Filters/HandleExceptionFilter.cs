using Mvc5Application1.Extensions;
using Mvc5Application1.Framework.Logging;
using System;
using System.Web.Mvc;

namespace Mvc5Application1.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class HandleExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private Type exceptionType;
        private readonly ILogger logger;

        public HandleExceptionFilter(ILogger logger)
        {
            this.logger = logger;
            exceptionType = typeof(Exception);
        }

        public virtual void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            Exception exception = filterContext.Exception;
            if (!filterContext.ExceptionHandled && ExceptionType.IsInstanceOfType(exception))
            {
                var message = ErrorMessage ?? exception.Message;
                filterContext.Controller.ViewData.ModelState.AddModelError("", message);

                filterContext.CreateViewResult();

                filterContext.ExceptionHandled = true;
                logger.LogInformational(filterContext.Exception.Message);
            }
        }

        public Type ExceptionType
        {
            get
            {
                return exceptionType;
            }
            set
            {
                if (value == null || !typeof(Exception).IsAssignableFrom(value))
                {
                    throw new ArgumentException("value");
                }
                exceptionType = value;
            }
        }

        public string ErrorMessage { get; set; }
    }
}