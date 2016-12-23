using System;
using System.Security;
using System.Security.Permissions;

namespace Mvc5Application1.Framework.Security.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class Mvc5Application1PermissionAttribute : CodeAccessSecurityAttribute
    {
        /// <summary>
        /// Gets or sets the resource name.
        /// </summary>
        /// <value>
        /// The resource name.
        /// </value>
        public string Function { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mvc5Application1PermissionAttribute"/> class.
        /// </summary>
        /// <param name="securityAction">One of the <see cref="T:System.Security.Permissions.SecurityAction"/> values.</param>
        public Mvc5Application1PermissionAttribute(SecurityAction securityAction = SecurityAction.Demand)
            : base(securityAction)
        {
        }

        /// <summary>
        /// When overridden in a derived class, creates a permission object that can then be serialized into binary form and persistently stored along with the <see cref="T:System.Security.Permissions.SecurityAction"/> in an assembly's metadata.
        /// </summary>
        /// <returns>
        /// A serializable permission object.
        /// </returns>
        public override IPermission CreatePermission()
        {
            return new Mvc5Application1Permission(Function);
        }
    }
}