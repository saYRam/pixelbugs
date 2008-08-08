using System.Security;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages.Impl;

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
        /// Gets a user's permissions from its id
        /// </summary>
        /// <param name="request">The request that contains the id</param>
        /// <returns>Returns a response object that contains the users permissions</returns>
        /// <exception>An <see cref="InvalidRequestException"/> is thrown if the id is invalid</exception>
        RetrieveUserPermissionsResponse RetrieveUserPermissions(RetrieveUserPermissionsRequest request);
    }
}