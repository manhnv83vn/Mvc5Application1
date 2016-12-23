using Mvc5Application1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Mvc5Application1.CustomeAnnotation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckBoxListAtLeastOneItemCheckedAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            var instance = value as IEnumerable<CheckboxListItem>;
            if (instance != null)
            {
                return instance.Any(x => x.IsSelected);
            }

            return base.IsValid(value);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage,
                ValidationType = "mustbetrue"
            };
        }
    }
}