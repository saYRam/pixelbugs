using System;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using System.Security;
using System.Web.Security;

namespace PixelDragons.PixelBugs.Core.Services
{
    public class SimpleSecurityService : ISecurityService
    {
        #region Fields
        private IUserRepository _userRepository;
        #endregion

        #region Constructors
        public SimpleSecurityService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        public string Authenticate(string userName, string password)
        {
            User user = _userRepository.FindByUserNameAndPassword(userName, password);

            if (user == null)
            {
                throw (new SecurityException("Invalid user name or password"));
            }

            return user.Id.ToString();
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
