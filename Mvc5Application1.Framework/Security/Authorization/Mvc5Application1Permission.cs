using Microsoft.Practices.ServiceLocation;
using System;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Mvc5Application1.Framework.Security.Authorization
{
    /// <summary>
    /// Encapsulates calls to AuthorizationManager with custom claim types in a CLR permission
    /// </summary>
    [Serializable]
    public sealed class Mvc5Application1Permission : IPermission, IUnrestrictedPermission
    {
        /// <summary>
        /// Gets or sets the resource name.
        /// </summary>
        /// <value>The resource name.</value>
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets the operation name .
        /// </summary>
        /// <value>The operation name.</value>
        public string Operation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mvc5Application1Permission" /> class.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="operation">The operation.</param>
        public Mvc5Application1Permission(string resource)
        {
            Resource = resource;
        }

        /// <summary>
        /// Creates and returns an identical copy of the current permission.
        /// </summary>
        /// <returns>A copy of the current permission.</returns>
        public IPermission Copy()
        {
            return new Mvc5Application1Permission(Resource);
        }

        /// <summary>
        /// Throws a <see cref="T:System.Security.SecurityException" /> at run time if the security requirement is not met.
        /// </summary>
        public void Demand()
        {
            var claimsAuthorizationManager = ServiceLocator.Current.GetInstance<AuthorizationManager>();

            if (!claimsAuthorizationManager.CheckAccess(Resource))
            {
                ThrowSecurityException();
            }
        }

        /// <summary>
        /// Throws a <see cref="T:System.Security.SecurityException" /> at run time if the security requirement is not met.
        /// This is static version.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="operation">The operation.</param>
        public static void Demand(string resource, string operation)
        {
            var claimPermission = new Mvc5Application1Permission(resource);
            claimPermission.Demand();
        }

        /// <summary>
        /// Checks the access.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="operation">The operation.</param>
        /// <returns><c>true</c> if the current principal has permission to do the operation on the resource, <c>false</c> otherwise</returns>
        public static bool CheckAccess(string resource)
        {
            var claimsAuthorizationManager = ServiceLocator.Current.GetInstance<AuthorizationManager>();

            return claimsAuthorizationManager.CheckAccess(resource.Trim());
        }

        #region CLR Permission Implementation

        /// <summary>
        /// Reconstructs a security object with a specified state from an XML encoding.
        /// </summary>
        /// <param name="e">The XML encoding to use to reconstruct the security object.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void FromXml(SecurityElement e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates and returns a permission that is the intersection of the current permission and the specified permission.
        /// </summary>
        /// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
        /// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
        /// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not an instance of the same class as the current permission.</exception>
        public IPermission Intersect(IPermission target)
        {
            if (target == null)
            {
                return null;
            }

            var permission = target as Mvc5Application1Permission;
            if (permission == null)
            {
                return null;
            }

            if (permission.Resource == Resource && permission.Operation == Operation)
            {
                return new Mvc5Application1Permission(Resource);
            }

            return null;
        }

        /// <summary>
        /// Determines whether the current permission is a subset of the specified permission.
        /// </summary>
        /// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
        /// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission.</exception>
        public bool IsSubsetOf(IPermission target)
        {
            if (target == null)
            {
                return false;
            }

            var permission = target as Mvc5Application1Permission;
            if (permission == null)
            {
                return false;
            }

            return (permission.Resource == Resource && permission.Operation == Operation);
        }

        /// <summary>
        /// Returns a value indicating whether unrestricted access to the resource protected by the permission is allowed.
        /// </summary>
        /// <returns>true if unrestricted use of the resource protected by the permission is allowed; otherwise, false.</returns>
        public bool IsUnrestricted()
        {
            return true;
        }

        /// <summary>
        /// Throws the security exception.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">null;null;null</exception>
        private void ThrowSecurityException()
        {
            string errorMesssage = string.Format("Access Denied: You don't have permission to do '{0}' resource.", Resource);
            throw new ApplicationException(errorMesssage);
        }

        /// <summary>
        /// Creates an XML encoding of the security object and its current state.
        /// </summary>
        /// <returns>An XML encoding of the security object, including any state information.</returns>
        public SecurityElement ToXml()
        {
            SecurityElement element = new SecurityElement("IPermission");
            Type type = GetType();
            StringBuilder builder = new StringBuilder(type.Assembly.ToString());
            builder.Replace('"', '\'');
            element.AddAttribute("class", type.FullName + ", " + builder);
            element.AddAttribute("version", "1");
            SecurityElement child = new SecurityElement("ResourceOperation");
            child.AddAttribute("resource", Resource);
            child.AddAttribute("operation", Operation);
            element.AddChild(child);
            return element;
        }

        /// <summary>
        /// Creates a permission that is the union of the current permission and the specified permission.
        /// </summary>
        /// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
        /// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
        /// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission.</exception>
        public IPermission Union(IPermission target)
        {
            if (target == null)
            {
                return null;
            }

            var permission = target as Mvc5Application1Permission;
            if (permission == null)
            {
                return null;
            }
            return new Mvc5Application1Permission(Resource);
        }
    }

        #endregion CLR Permission Implementation
}