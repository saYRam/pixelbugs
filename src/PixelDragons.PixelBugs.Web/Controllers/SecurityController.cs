using System;
using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Web.Controllers
{
    [Layout("Default"), Rescue("GeneralError")]
    [Resource("strings", "PixelDragons.PixelBugs.Web.Resources.Controllers.SecurityController")]
    public class SecurityController : ARSmartDispatcherController
    {
        #region Properties
        private IUserRepository UserRepository { get; set; }
        #endregion

        #region Constructors
        public SecurityController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        #endregion

        public void Index()
        {
        }

        public void Authenticate(string userName, string password)
        {
            User user = UserRepository.FindByUserNameAndPassword(userName, password);

            if (user != null)
            {
                Session["currentUserId"] = user.Id;
                Redirect("Issue", "List");
            }
            else
            {
                Session["currentUserId"] = null;
                Flash["error"] = "InvalidCredentials";
                RedirectToAction("Index");
            }
        }

        public void Timeout()
        { 
        }

        public void AccessDenied()
        {
        }
    }
}
