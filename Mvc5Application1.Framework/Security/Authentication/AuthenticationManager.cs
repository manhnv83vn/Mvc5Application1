using System;
using System.Threading;

namespace Mvc5Application1.Framework.Security.Authentication
{
    /// <summary>
    /// Class AuthenticationManager
    /// </summary>
    public class AuthenticationManager
    {
        /// <summary>
        /// Gets or sets the authentication service.
        /// </summary>
        /// <value>The authentication service.</value>
        public IAuthenticationService AuthenticationService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationManager"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        public AuthenticationManager(IAuthenticationService authenticationService)
        {
            AuthenticationService = authenticationService;
        }

        /// <summary>
        /// Authenticates the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        /// <exception cref="System.ArgumentNullException">
        /// userName
        /// or
        /// password
        /// </exception>
        public bool Authenticate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            var isAutheticate = AuthenticationService.Authenticate(userName, password);
            if (isAutheticate)
            {
                var roles = AuthenticationService.GetGroups(userName);
                InitialisePrincipal(userName, roles);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Initialises the principal.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="roles">The roles.</param>
        /// <exception cref="System.ArgumentNullException">userName</exception>
        private void InitialisePrincipal(string userName, string[] roles)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            var identity = new Mvc5Application1Identity(userName);
            var principal = new Mvc5Application1Principal(identity, roles);
            Thread.CurrentPrincipal = principal;
        }
    }
}