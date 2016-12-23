using System.Security.Principal;

namespace Mvc5Application1.Framework.Security
{
    /// <summary>
    /// Class Mvc5Application1Principal
    /// </summary>
    public class Mvc5Application1Principal : GenericPrincipal
    {
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public string[] Roles { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mvc5Application1Principal"/> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="roles">The roles.</param>
        public Mvc5Application1Principal(Mvc5Application1Identity identity, string[] roles)
            : base(identity, roles)
        {
            Roles = roles;
        }
    }
}