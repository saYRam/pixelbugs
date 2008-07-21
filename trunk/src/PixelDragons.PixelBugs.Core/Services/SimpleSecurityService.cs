using System;
using System.Collections.Generic;
using System.Security;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages;
using PixelDragons.PixelBugs.Core.Queries.Users;

namespace PixelDragons.PixelBugs.Core.Services
{
    public class SimpleSecurityService : ISecurityService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRetrievedUserPermissionsMapper userPermissionsMapper;

        public SimpleSecurityService(IRepository<User> userRepository, IRetrievedUserPermissionsMapper userPermissionsMapper)
        {
            this.userRepository = userRepository;
            this.userPermissionsMapper = userPermissionsMapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            request.Validate();
            
            UserAuthenticationQuery query = new UserAuthenticationQuery(request);

            User[] users = userRepository.Find(query);

            if (users.Length == 0)
                throw (new SecurityException("Invalid user name or password"));

            return new AuthenticateResponse(users[0].Id);
        }

        public RetrieveUserPermissionsResponse RetrieveUserPermissions(RetrieveUserPermissionsRequest permissionsRequest)
        {
            permissionsRequest.Validate();

            try
            {
                User user = userRepository.FindById(permissionsRequest.Id);
                return userPermissionsMapper.MapFrom(user);
            }
            catch(Exception)
            {
                throw new InvalidRequestException( string.Format("Unable to find a user with the supplied id of '{0}'", permissionsRequest.Id));
            }
        }
    }
}