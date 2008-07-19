using System;
using System.Collections.Generic;
using System.Security;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;
using PixelDragons.PixelBugs.Core.Queries.Users;

namespace PixelDragons.PixelBugs.Core.Services
{
    public class SimpleSecurityService : ISecurityService
    {
        private readonly IRepository<User> userRepository;

        public SimpleSecurityService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
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

        public RetrieveUserResponse RetrieveUser(RetrieveUserRequest request)
        {
            request.Validate();

            try
            {
                User user = userRepository.FindById(request.Id);

                List<Permission> permissions = new List<Permission>();

                if (user.Roles != null)
                {
                    foreach (Role role in user.Roles)
                    {
                        permissions.AddRange(role.Permissions);
                    }
                }

                return new RetrieveUserResponse(user.Id, permissions, user.FullName);
            }
            catch(Exception)
            {
                throw new InvalidRequestException( string.Format("Unable to find a user with the supplied id of '{0}'", request.Id));
            }
        }
    }
}