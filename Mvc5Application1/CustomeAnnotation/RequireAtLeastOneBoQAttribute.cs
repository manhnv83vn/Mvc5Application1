using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc5Application1.CustomeAnnotation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequireAtLeastOneBoQAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
    }
}