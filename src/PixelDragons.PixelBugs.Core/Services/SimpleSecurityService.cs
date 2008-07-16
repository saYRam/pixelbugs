using System;
using System.Security;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Messages.SecurityService;
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

        public AuthenticateUserResponse Authenticate(AuthenticateUserRequest request)
        {
            request.Validate();
            
            UserAuthenticationQuery query = new UserAuthenticationQuery(request);

            User[] users = userRepository.Find(query);

            if (users.Length == 0)
                throw (new SecurityException("Invalid user name or password"));

            return new AuthenticateUserResponse(users[0].Id.ToString());
        }

        public User GetAuthenticatedUserFromToken(string token)
        {
            try
            {
                return userRepository.FindById(new Guid(token));
            }
            catch (Exception)
            {
                //Token was either null or invalid
                return null;
            }
        }
    }
}