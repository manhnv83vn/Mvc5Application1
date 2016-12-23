using Mvc5Application1.Resources;
using System;

namespace Mvc5Application1.CustomeAnnotation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayPromptAttribute : Attribute
    {
        public DisplayPromptAttribute(string value)
        {
            Value = value;
        }

        public DisplayPromptAttribute()
            : this(Message.Dropdownlist_Select)
        {
        }

        public string Value { get; set; }
    }
}