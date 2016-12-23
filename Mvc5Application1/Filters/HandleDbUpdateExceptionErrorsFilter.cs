using Mvc5Application1.Extensions;
using Mvc5Application1.Framework.Logging;
using Mvc5Application1.Resources;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web.Mvc;

namespace Mvc5Application1.Filters
{
    public class HandleDbUpdateExceptionErrorsFilter : FilterAttribute, IExceptionFilter
    {
        private readonly ILogger logger;
        private readonly IDictionary<string, string> constrainMessageCodeMapper;

        public HandleDbUpdateExceptionErrorsFilter(ILogger logger)
        {
            this.logger = logger;
            constrainMessageCodeMapper = GetMapper();
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is DbUpdateException)
            {
                var sqlException = filterContext.Exception.GetBaseException();
                if (sqlException is SqlException)
                {
                    var errorMessage = sqlException.Message;
                    var messageCode = GetMessageCode(errorMessage);
                    if (!string.IsNullOrEmpty(messageCode))
                    {
                        filterContext.Controller.ViewData.ModelState.AddModelError("", Message.ResourceManager.GetString(messageCode, CultureInfo.CurrentUICulture));

                        filterContext.CreateViewResult();

                        filterContext.ExceptionHandled = true;

                        logger.LogInformational(filterContext.Exception.Message);
                    }
                }
            }
        }

        private string GetMessageCode(string errorMessage)
        {
            return (constrainMessageCodeMapper.Where(keyValuePair => errorMessage.Contains(keyValuePair.Key))
                .Select(keyValuePair => keyValuePair.Value)).FirstOrDefault();
        }

        private static IDictionary<string, string> GetMapper()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            ResourceManager rm = DbConstraintMessageCodeMapper.ResourceManager;

            ResourceSet rs = rm.GetResourceSet(CultureInfo.InvariantCulture, true, false);

            if (rs != null)
            {
                IDictionaryEnumerator de = rs.GetEnumerator();
                while (de.MoveNext())
                {
                    var key = de.Key.ToString();
                    result[key] = rm.GetString(key, CultureInfo.InvariantCulture);
                }
            }
            return result;
        }
    }
}