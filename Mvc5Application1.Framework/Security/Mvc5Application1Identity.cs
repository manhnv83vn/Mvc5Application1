using System.Security.Principal;

namespace Mvc5Application1.Framework.Security
{
    /// <summary>
    /// Class Mvc5Application1Identity
    /// </summary>
    public class Mvc5Application1Identity : IIdentity
    {
        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <value>The name.</value>
        /// <returns>The name of the user on whose behalf the code is running.</returns>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <value>The type of the authentication.</value>
        /// <returns>The type of authentication used to identify the user.</returns>
        public string AuthenticationType { get; private set; }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <value><c>true</c> if this instance is authenticated; otherwise, <c>false</c>.</value>
        /// <returns>true if the user was authenticated; otherwise, false.</returns>
        public bool IsAuthenticated { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mvc5Application1Identity"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public Mvc5Application1Identity(string userName)
        {
            Name = userName;
            AuthenticationType = "Custom";
            IsAuthenticated = true;
        }
    }
}