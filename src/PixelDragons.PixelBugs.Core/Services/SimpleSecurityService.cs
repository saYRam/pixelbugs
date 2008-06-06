using System;
using PixelDragons.PixelBugs.Core.Domain;
using System.Security;
using System.Web.Security;
using PixelDragons.Commons.Repositories;
using NHibernate.Criterion;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Core.Services
{
    public class SimpleSecurityService : ISecurityService
    {
        #region Fields
        private IRepository<User> _userRepository;
        private IUserQueries _userQueries;
        #endregion

        #region Constructors
        public SimpleSecurityService(IRepository<User> userRepository, IUserQueries userQueries)
        {
            _userRepository = userRepository;
            _userQueries = userQueries;
        }
        #endregion

        public string Authenticate(string userName, string password)
        {
            DetachedCriteria criteria = _userQueries.BuildAuthenticationQuery(userName, password);

            User[] users = _userRepository.FindAll(criteria);

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
                return _userRepository.FindById(new Guid(token));
            }
            catch (Exception)
            {
                //Token was either null or invalid
                return null;
            }
        }
    }
}
