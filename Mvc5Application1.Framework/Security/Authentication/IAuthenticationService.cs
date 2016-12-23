namespace Mvc5Application1.Framework.Security.Authentication
{
    /// <summary>
    /// Interface IAuthenticationService
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        bool Authenticate(string userName, string password);

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>System.String[][].</returns>
        string[] GetGroups(string userName);
    }
}