﻿using System;
using System.Web.Mvc;

namespace Mvc5Application1.Filters
{
    /// <summary>
    /// An Action Filter for exporting ModelState to TempData so it is available on the next request.
    /// </summary>
    /// <remarks>
    /// Useful when following the PRG (Post, Redirect, Get) pattern.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExportModelStateToTempDataAttribute : ModelStateTempDataTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Only copy when ModelState is invalid and we're performing a Redirect (i.e. PRG)
            if (!filterContext.Controller.ViewData.ModelState.IsValid && (filterContext.Result is RedirectResult || filterContext.Result is RedirectToRouteResult))
            {
                ExportModelStateToTempData(filterContext);
            }

            base.OnActionExecuted(filterContext);
        }
    }
}