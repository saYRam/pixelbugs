using System.Security;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Messages.SecurityService;

namespace PixelDragons.PixelBugs.Core.Services
{
    public interface ISecurityService
    {
        /// <summary>
        /// Authenticates a user from their user name and password and returns an security token
        /// </summary>
        /// <param name="request">The authenticate user request object</param>
        /// <returns>Returns a security token as a string</returns>
        /// <exception cref="System.Security.SecurityException">A <see cref="SecurityException"/> is thrown if the authentication fails</exception>
        AuthenticateUserResponse Authenticate(AuthenticateUserRequest request);

        /// <summary>
        /// Gets an authenticated user from a security token
        /// </summary>
        /// <param name="token">The security token</param>
        /// <returns>Returns the authenticated user if the security token is valid, otherwise returns null</returns>
        User GetAuthenticatedUserFromToken(string token);
    }
}