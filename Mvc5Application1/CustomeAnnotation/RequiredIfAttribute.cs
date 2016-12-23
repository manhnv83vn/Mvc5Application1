using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Mvc5Application1.CustomeAnnotation
{
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly RequiredAttribute innerAttribute = new RequiredAttribute();

        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }

        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            DependentProperty = dependentProperty;
            TargetValue = targetValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(DependentProperty);

            if (field != null)
            {
                var dependentvalue = field.GetValue(validationContext.ObjectInstance, null);

                if ((dependentvalue == null && TargetValue == null) ||
                    (dependentvalue != null && dependentvalue.Equals(TargetValue)))
                {
                    if (!innerAttribute.IsValid(value))
                        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "requiredif"
            };

            string depProp = BuildDependentPropertyId(metadata, context as ViewContext);

            string targetValue = (TargetValue ?? "").ToString();

            if (TargetValue is bool)
            {
                targetValue = targetValue.ToLower();
            }

            rule.ValidationParameters.Add("dependentproperty", depProp);
            rule.ValidationParameters.Add("targetvalue", targetValue);

            yield return rule;
        }

        private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext viewContext)
        {
            string depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(DependentProperty);
            if (depProp.StartsWith(metadata.PropertyName + "_"))
            {
                depProp = depProp.Substring((metadata.PropertyName + "_").Length);
            }

            if (depProp.StartsWith(metadata.PropertyName + "."))
            {
                depProp = depProp.Substring((metadata.PropertyName + ".").Length);
            }

            return depProp;
        }
    }
}