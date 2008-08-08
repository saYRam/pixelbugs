using System;
using System.Security;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages.Impl;
using PixelDragons.PixelBugs.Core.Queries.Users;

namespace PixelDragons.PixelBugs.Core.Services.Impl
{
    public class SimpleSecurityService : ISecurityService
    {
        private readonly IRepository<User> userRepository;
        private readonly IUserPermissionsDTOMapper userPermissionsDTOMapper;

        public SimpleSecurityService(IRepository<User> userRepository, IUserPermissionsDTOMapper userPermissionsDTOMapper)
        {
            this.userRepository = userRepository;
            this.userPermissionsDTOMapper = userPermissionsDTOMapper;
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
                UserPermissionsDTO dto = userPermissionsDTOMapper.MapFrom(user);
                return new RetrieveUserPermissionsResponse(dto);
            }
            catch(Exception)
            {
                throw new InvalidRequestException( string.Format("Unable to find a user with the supplied id of '{0}'", permissionsRequest.Id));
            }
        }
    }
}