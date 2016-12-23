using System;
using System.Linq;
using System.Reflection;

namespace Mvc5Application1.Infrastructure.Extenstions
{
    public static class AttributeExtensions
    {
        public static TAttribute GetAttributeOnTypeOrAssembly<TAttribute>(this Type type) where TAttribute : System.Attribute
        {
            return type.First<TAttribute>() ?? type.Assembly.First<TAttribute>();
        }

        public static TAttribute First<TAttribute>(this ICustomAttributeProvider attributeProvider)
            where TAttribute : System.Attribute
        {
            return attributeProvider.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
        }
    }
}