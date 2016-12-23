using Mvc5Application1.Filters;
using Mvc5Application1.Framework.Logging;
using System.Security;
using System.Web;
using System.Web.Mvc;

namespace Mvc5Application1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var logger = DependencyResolver.Current.GetService<ILogger>();
            filters.Add(new AuthorizeAttribute());

            //Handle SecurityException
            filters.Add(new HandleExceptionFilter(logger)
            {
                ExceptionType = typeof(SecurityException)
            });

            filters.Add(new HandleExceptionFilter(logger)
            {
                ExceptionType = typeof(HttpRequestValidationException)
            });

            filters.Add(new HandleExceptionFilter(logger)
            {
                ExceptionType = typeof(HttpAntiForgeryException)
            });

            filters.Add(new Mvc5Application1ExceptionFilter(logger));

            //Handle validation exception thrown by EF
            filters.Add(new MapEntityExceptionsToModelErrorsFilter(logger));
            filters.Add(new HandleDbUpdateConcurrencyExceptionFilter(logger));

            //Handle exception thrown by db server
            filters.Add(new HandleDbUpdateExceptionErrorsFilter(logger));

            filters.Add(new RestoreMvc5Application1PrincipalFilter());
        }
    }
}