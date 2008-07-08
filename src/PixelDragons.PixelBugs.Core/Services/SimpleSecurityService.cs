using System;
using System.Security;
using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Core.Services
{
    public class SimpleSecurityService : ISecurityService
    {
        private readonly IRepository<User> userRepository;
        private readonly IUserQueries userQueries;

        public SimpleSecurityService(IRepository<User> userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }

        public string Authenticate(string userName, string password)
        {
            DetachedCriteria criteria = userQueries.BuildAuthenticationQuery(userName, password);

            User[] users = userRepository.FindAll(criteria);

            if (users.Length == 0)
            {
                throw (new SecurityException("Invalid user name or password"));
            }

            return users[0].Id.ToString();
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