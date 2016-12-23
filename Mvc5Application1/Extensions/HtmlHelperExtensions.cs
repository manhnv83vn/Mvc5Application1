using Mvc5Application1.Business.Contracts.RefData;
using Mvc5Application1.CustomeAnnotation;
using Mvc5Application1.Data.Model;
using Mvc5Application1.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Mvc5Application1.Extensions
{
    public class EntryModel
    {
        public string PropertyName { get; set; }
        public string ControlId { get; set; }
        public MvcHtmlString Label { get; set; }
        public MvcHtmlString Input { get; set; }
        public string JsScript { get; set; }
        public MvcHtmlString ValidationMessage { get; set; }
        public bool Required { get; set; }
        public string Command { get; set; }
        public object PropertyValue { get; set; }
    }

    public class LinkModel
    {
        public MvcHtmlString Label { get; set; }
        public MvcHtmlString Url { get; set; }
    }

    public class RadioModel
    {
        public MvcHtmlString Label { get; set; }
        public MvcHtmlString InputYes { get; set; }
        public MvcHtmlString InputNo { get; set; }
    }

    public static class HtmlHelperExtensions
    {
        #region Public mothods

        public static MvcHtmlString EntryDate<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>>
                expression,
            object attributes = null)
        {
            Action<EntryModel, IDictionary<string, object>> processor =
                (model, htmlAttributes) =>
                {
                    htmlAttributes["data-date-format"] = CommonString.DateFormat;
                    model.Input = htmlHelper.TextBoxFor(expression, "{0:dd-MMM-yyyy}", htmlAttributes);
                };

            return EntryFor(htmlHelper, "Controls/FormEntryDate", expression, processor, "calendar", attributes);
        }

        public static MvcHtmlString EntryCheckbox<TModel>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>>
                expression,
            object attributes = null)
        {
            Action<EntryModel, IDictionary<string, object>> processor =
                (model, htmlAttributes) =>
                {
                    model.Required = false;
                    model.Input = htmlHelper.CheckBoxFor(expression, htmlAttributes);
                };
            return htmlHelper.EntryFor(expression, processor, null, attributes);
        }

        public static MvcHtmlString EntryRadio<TModel>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool?>>
                expression,
            object attributes = null)
        {
            return htmlHelper.Partial("Controls/RadioEntry", new RadioModel
            {
                Label = htmlHelper.LabelFor(expression),
                InputYes =
                    htmlHelper.RadioButtonFor(expression, "true"),
                InputNo =
                    htmlHelper.RadioButtonFor(expression, "false")
            });
        }

        public static MvcHtmlString EntryTextArea<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>>
                expression,
            string command,
            object attributes = null)
        {
            Action<EntryModel, IDictionary<string, object>> processor =
                (model, htmlAttributes) =>
                {
                    htmlAttributes["class"] = "form-control";
                    model.Input = htmlHelper.TextAreaFor(expression, htmlAttributes);
                };

            return htmlHelper.EntryFor(expression, processor, command, attributes);
        }

        public static MvcHtmlString EntryTextArea<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>>
                expression,
            object attributes = null)
        {
            return EntryTextArea(htmlHelper, expression, null, attributes);
        }

        public static MvcHtmlString EntryTextAreaWithLookup<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>>
                expression,
            object attributes = null)
        {
            return EntryTextArea(htmlHelper, expression, "lookup", attributes);
        }

        public static MvcHtmlString EntryText<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>>
                expression,
            object attributes = null)
        {
            Action<EntryModel, IDictionary<string, object>> processor =
                (model, htmlAttributes) =>
                {
                    htmlAttributes["class"] = "form-control";
                    model.Input = htmlHelper.TextBoxFor(expression, htmlAttributes);
                };

            return htmlHelper.EntryFor(expression, processor, null, attributes);
        }

        public static MvcHtmlString EntryUpperCaseText<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>>
                expression,
            object attributes = null)
        {
            Action<EntryModel, IDictionary<string, object>> processor =
                (model, htmlAttributes) =>
                {
                    htmlAttributes["class"] = "form-control";
                    htmlAttributes["style"] = " text-transform: uppercase";
                    model.Input = htmlHelper.TextBoxFor(expression, htmlAttributes);
                };

            return htmlHelper.EntryFor(expression, processor, null, attributes);
        }

        public static MvcHtmlString EntryPassword<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>>
                expression,
            object attributes = null)
        {
            return htmlHelper.EntryFor(expression, InputExtensions.PasswordFor, null, attributes);
        }

        public static MvcHtmlString EntryTextWithCommand<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string command,
            object attributes = null)
        {
            return htmlHelper.EntryFor(expression, InputExtensions.TextBoxFor, command, attributes);
        }

        public static MvcHtmlString EntryTextWithLookup<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
                                                                        Expression<Func<TModel, TValue>> expression,
                                                                        object attributes = null)
        {
            return htmlHelper.EntryFor(expression, InputExtensions.TextBoxFor, "lookup", attributes);
        }

        public static MvcHtmlString EntrySelect<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectList,
            string prompt,
            object attributes = null)
        {
            var member = expression.Body as MemberExpression;
            if (prompt == null)
            {
                var promptAttribute = member.Member
                                          .GetCustomAttributes(typeof(DisplayPromptAttribute), false)
                                          .FirstOrDefault() as DisplayPromptAttribute;
                prompt = promptAttribute != null ? promptAttribute.Value : null;
            }

            Action<EntryModel, IDictionary<string, object>> processor =
                (model, htmlAttributes) =>
                {
                    model.Input = htmlHelper.DropDownListFor(expression, selectList, prompt, htmlAttributes);
                };

            return htmlHelper.EntryFor("Controls/FormEntry", expression, processor, null, attributes);
        }

        public static MvcHtmlString EntrySelect<TModel, TValue>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression,
           IEnumerable<SelectListItem> selectList,
           object attributes = null)
        {
            return EntrySelect(htmlHelper, expression, selectList, null, attributes);
        }

        public static MvcHtmlString EntrySelect<TModel, TValue, TRef>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            Func<IRefDataBusiness, IEnumerable<TRef>> selectListFunc,
            object attributes = null)
        {
            var selectList = ReferenceDataHelper.ToSelectListItems(selectListFunc);
            return EntrySelect(htmlHelper, expression, selectList, null, attributes);
        }

        public static MvcHtmlString EntrySelectWithAddCommand<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          IEnumerable<SelectListItem> selectList,
          string prompt,
          object attributes = null)
        {
            var member = expression.Body as MemberExpression;
            if (prompt == null)
            {
                var promptAttribute = member.Member
                                          .GetCustomAttributes(typeof(DisplayPromptAttribute), false)
                                          .FirstOrDefault() as DisplayPromptAttribute;
                prompt = promptAttribute != null ? promptAttribute.Value : null;
            }

            Action<EntryModel, IDictionary<string, object>> processor =
                (model, htmlAttributes) =>
                {
                    model.Input = htmlHelper.DropDownListFor(expression, selectList, prompt, htmlAttributes);
                };

            return htmlHelper.EntryFor("Controls/FormEntryWithAddCommand", expression, processor, null, attributes);
        }

        public static MvcHtmlString EntrySelectWithAddCommand<TModel, TValue>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression,
           IEnumerable<SelectListItem> selectList,
           object attributes = null)
        {
            return EntrySelectWithAddCommand(htmlHelper, expression, selectList, null, attributes);
        }

        public static MvcHtmlString EntrySelectWithAddCommand<TModel, TValue, TRef>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            Func<IRefDataBusiness, IEnumerable<TRef>> selectListFunc,
            object attributes = null)
        {
            var selectList = ReferenceDataHelper.ToSelectListItems(selectListFunc);
            return EntrySelectWithAddCommand(htmlHelper, expression, selectList, null, attributes);
        }

        public static MvcHtmlString EntrySelectWithOutLabel<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectList,
            string prompt,
            object attributes = null)
        {
            var member = expression.Body as MemberExpression;
            if (prompt == null)
            {
                var promptAttribute = member.Member
                                          .GetCustomAttributes(typeof(DisplayPromptAttribute), false)
                                          .FirstOrDefault() as DisplayPromptAttribute;
                prompt = promptAttribute != null ? promptAttribute.Value : null;
            }

            Action<EntryModel, IDictionary<string, object>> processor =
                (model, htmlAttributes) =>
                {
                    model.Input = htmlHelper.DropDownListFor(expression, selectList, prompt, htmlAttributes);
                };

            return htmlHelper.EntryFor("Controls/FormEntryWithOutLabel", expression, processor, null, attributes);
        }

        public static MvcHtmlString EntrySelectWithoutLabel<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectList,
            object attributes = null)
        {
            return EntrySelectWithOutLabel(htmlHelper, expression, selectList, null, attributes);
        }

        public static MvcHtmlString EntryLink<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression)
        {
            var model = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
            string url = null;

            if (model != null)
            {
                url = model.ToString();
            }

            return htmlHelper.Partial("Controls/LinkEntry", new LinkModel
            {
                Label = htmlHelper.LabelFor(expression),
                Url = new MvcHtmlString(url)
            });
        }

        #endregion Public mothods

        #region Private methods

        private static MvcHtmlString EntryFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            Action<EntryModel, IDictionary<string, object>> processor,
            string command = null,
            object attributes = null,
            object extraAttributes = null)
        {
            return EntryFor(htmlHelper,
                            String.IsNullOrWhiteSpace(command) ? "Controls/FormEntry" : "Controls/FormEntryWithCommand",
                            expression, processor, command, attributes, extraAttributes);
        }

        private static MvcHtmlString EntryFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            Func<HtmlHelper<TModel>, Expression<Func<TModel, TValue>>, IDictionary<string, object>, MvcHtmlString>
                renderFunction,
            string command = null,
            object attributes = null,
            object extraAttributes = null)
        {
            Action<EntryModel, IDictionary<string, object>> processor =
                (model, htmlAttributes) =>
                {
                    model.Input = renderFunction(htmlHelper, expression, htmlAttributes);
                };

            return EntryFor(htmlHelper, expression, processor, command, attributes, extraAttributes);
        }

        private static MvcHtmlString EntryFor<TModel, TValue>
            (this HtmlHelper<TModel> htmlHelper,
             string partialView,
             Expression<Func<TModel, TValue>> expression,
             Action<EntryModel, IDictionary<string, object>> processor,
             string command = null,
             object attributes = null,
             object extraAttributes = null)
        {
            var member = expression.Body as MemberExpression;
            var stringLength = member.Member
                                   .GetCustomAttributes(typeof(StringLengthAttribute), false)
                                   .FirstOrDefault() as StringLengthAttribute;

            var htmlAttributes = ToDictionary(attributes);
            if (stringLength != null)
            {
                htmlAttributes["maxlength"] = stringLength.MaximumLength;
            }

            var isDisabled = htmlAttributes["disabled"] != null;

            if (extraAttributes != null)
            {
                var extraHtmlAttributes = ToDictionary(extraAttributes);
                foreach (var attribute in extraHtmlAttributes)
                {
                    htmlAttributes.Add(attribute);
                }
            }

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            //find control Id
            var expressionStr = expression.Body.ToString();
            var controlId = expressionStr.Substring(expressionStr.IndexOf('.') + 1).Replace('.', '_');

            var model = new EntryModel
                            {
                                PropertyName = metadata.PropertyName,
                                ControlId = controlId,
                                Label = htmlHelper.LabelFor(expression),
                                ValidationMessage = htmlHelper.ValidationMessageFor(expression),
                                Command = command,
                                Required = metadata.IsRequired && !isDisabled,
                                PropertyValue = metadata.Model
                            };
            processor(model, htmlAttributes);

            return htmlHelper.Partial(partialView, model);
        }

        private static IDictionary<string, object> ToDictionary(object attributes)
        {
            return new RouteValueDictionary(attributes);
        }

        #endregion Private methods
    }
}