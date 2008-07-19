using System.Security;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Services
{
    public interface ISecurityService
    {
        /// <summary>
        /// Authenticates a user from their user name and password and returns an security token
        /// </summary>
        /// <param name="request">The authenticate user request object</param>
        /// <returns>Returns a response object that contains a security token</returns>
        /// <exception cref="System.Security.SecurityException">A <see cref="SecurityException"/> is thrown if the authentication fails</exception>
        AuthenticateResponse Authenticate(AuthenticateRequest request);

        /// <summary>
        /// Gets user information from its id
        /// </summary>
        /// <param name="request">The request that contains the id</param>
        /// <returns>Returns a response object that contains the user information</returns>
        /// <exception>An <see cref="InvalidRequestException"/> is thrown if the id is invalid</exception>
        RetrieveUserResponse RetrieveUser(RetrieveUserRequest request);
    }
}