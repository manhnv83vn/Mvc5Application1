using System;
using System.Linq.Expressions;

namespace Mvc5Application1.Framework
{
    public class ObjectHelper
    {
        public static string PropertyName<T>(Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression ?? ((UnaryExpression)expression.Body).Operand as MemberExpression;

            return body != null ? body.Member.Name : string.Empty;
        }
    }
}