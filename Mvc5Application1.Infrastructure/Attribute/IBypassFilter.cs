using System;

namespace Mvc5Application1.Infrastructure.Attribute
{
    /// <summary>
    /// This interface must be supported on bypass filters if they are to use the customer filter provider bypass mechaism.
    /// The custom filter provider will look for bypass filter, i.e. filters that support this interface, then it will determine
    /// from this interface which filter is to be bypassed and remove it.
    /// </summary>
    public interface IBypassFilter
    {
        /// <summary>
        /// Gets the filter type to bypass so that it can be removed by the custom filter provider.
        /// </summary>
        /// <value>The filter to bypass.</value>
        Type FilterToBypass { get; }
    }
}