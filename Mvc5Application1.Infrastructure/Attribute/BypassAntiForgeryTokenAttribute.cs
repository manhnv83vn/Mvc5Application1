using System;
using System.Web.Mvc;

namespace Mvc5Application1.Infrastructure.Attribute
{
    /// <summary>
    /// Negates the anti forgery check on controllers. Use very carefully, as this could open up a security hole.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BypassAntiForgeryTokenAttribute : ActionFilterAttribute, IBypassFilter
    {
        /// <summary>
        /// Gets the filter type to bypass so that it can be removed by the custom filter provider.
        /// </summary>
        /// <value>The filter to bypass.</value>
        public Type FilterToBypass
        {
            get
            {
                return typeof(UseAntiForgeryTokenAttribute);
            }
        }
    }
}