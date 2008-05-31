using System;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Services
{
    public interface ISecurityService
    {
        /// <summary>
        /// Authenticates a user from their username and password and returns an security token
        /// </summary>
        /// <param name="userName">The user's username</param>
        /// <param name="password">The user's password</param>
        /// <returns>Returns a security token as a string</returns>
        /// <exception cref="System.Security.SecurityException">A SecurityException is thrown if the authentication fails</exception>
        string Authenticate(string userName, string password);

        /// <summary>
        /// Gets an authenticated user from a security token
        /// </summary>
        /// <param name="token">The security token</param>
        /// <returns>Returns the authenticated user if the security token is valid, otherwise returns null</returns>
        User GetAuthenticatedUserFromToken(string token);
    }
}
