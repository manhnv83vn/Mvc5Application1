using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Mvc5Application1.Unity
{
    /// <summary>
    /// Uses unity for service location and dependency resolution.
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {
        private const string HttpContextKey = "perRequestContainer";

        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <returns>
        /// The requested service or object.
        /// </returns>
        public object GetService(Type serviceType)
        {
            if (typeof(IController).IsAssignableFrom(serviceType))
            {
                return ChildContainer.Resolve(serviceType);
            }

            return ChildContainer.IsRegistered(serviceType) ? ChildContainer.Resolve(serviceType) : null;
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>
        /// The requested services.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ChildContainer.ResolveAll(serviceType);
        }

        /// <summary>
        /// Gets the child container.
        /// </summary>
        protected IUnityContainer ChildContainer
        {
            get
            {
                var childContainer = HttpContext.Current.Items[HttpContextKey] as IUnityContainer;

                if (childContainer == null)
                {
                    HttpContext.Current.Items[HttpContextKey] = childContainer = _container.CreateChildContainer();
                }

                return childContainer;
            }
        }

        /// <summary>
        /// Disposes the child container.
        /// </summary>
        public static void DisposeOfChildContainer()
        {
            var childContainer = HttpContext.Current.Items[HttpContextKey] as IUnityContainer;

            if (childContainer != null)
            {
                childContainer.Dispose();
            }
        }
    }
}