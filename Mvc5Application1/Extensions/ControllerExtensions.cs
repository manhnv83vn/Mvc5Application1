using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Mvc5Application1.Extensions
{
    /// <summary>
    ///     Extension methods to Controllers to add validation result from business object to model state
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        ///     Adds a model errors for each validation result from the business objects.
        /// </summary>
        /// <param name="controller">underline controller</param>
        /// <param name="validationResults">The validation results from a business object.</param>
        public static void AddModelErrors(this Controller controller, IEnumerable<ValidationResult> validationResults)
        {
            controller.ModelState.AddModelErrors(validationResults);
        }

        /// <summary>
        ///     Adds a model errors for each validation result from the business objects.
        /// </summary>
        /// <param name="validationResults">The validation results from a business object.</param>
        /// <param name="modelState">The model state dictionary used to add errors.</param>
        public static void AddModelErrors(this ModelStateDictionary modelState,
            IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults == null) return;

            foreach (ValidationResult validationResult in validationResults)
            {
                foreach (string memberName in validationResult.MemberNames)
                {
                    modelState.AddModelError(!string.IsNullOrEmpty(memberName) ? memberName : string.Empty,
                        validationResult.ErrorMessage);
                }
            }
        }

        public static string RenderViewToString(this ControllerBase controller, string viewName, object model)
        {
            var context = controller.ControllerContext;
            //Create memory writer
            var sb = new StringBuilder();
            var memWriter = new StringWriter(sb);

            //Create fake http context to render the view
            var fakeResponse = new HttpResponse(memWriter);
            var fakeContext = new HttpContext(HttpContext.Current.Request, fakeResponse);
            var fakeControllerContext = new ControllerContext(
                new HttpContextWrapper(fakeContext),
                context.RouteData, context.Controller);

            HttpContext oldContext = HttpContext.Current;
            HttpContext.Current = fakeContext;

            ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(context, viewName);

            var viewContext = new ViewContext(fakeControllerContext,
                viewResult.View, new ViewDataDictionary(), new TempDataDictionary(), memWriter);
            foreach (var item in viewContext.Controller.ViewData.ModelState)
            {
                if (!viewContext.ViewData.ModelState.Keys.Contains(item.Key))
                    viewContext.ViewData.ModelState.Add(item);
            }

            //Use HtmlHelper to render partial view to fake context
            var html = new HtmlHelper(viewContext,
                new ViewPage());
            html.RenderPartial(viewName, model);

            //Restore context
            HttpContext.Current = oldContext;

            //Flush memory and return output
            memWriter.Flush();
            return sb.ToString();
        }
    }
}