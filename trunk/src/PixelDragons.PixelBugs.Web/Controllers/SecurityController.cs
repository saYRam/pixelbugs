using System;
using System.Security;
using System.Web;
using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Services;

namespace PixelDragons.PixelBugs.Web.Controllers
{
    [Layout("Default"), Rescue("GeneralError")]
    [Resource("strings", "PixelDragons.PixelBugs.Web.Resources.Controllers.SecurityController")]
    public class SecurityController : ARSmartDispatcherController
    {
        #region Properties
        private ISecurityService _securityService;
        #endregion

        #region Constructors
        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }
        #endregion

        public void Index()
        {
            RenderView("Index");
        }

        public void Authenticate(string userName, string password)
        {
            try
            {
                string token = _securityService.Authenticate(userName, password);

                StoreTokenCookie(token);

                Redirect("Card", "Index");
            }
            catch(SecurityException)
            {
                Flash["error"] = "InvalidCredentials";

                ClearTokenCookieAndRedirect();
            }
        }

        public void SignOut()
        {
            ClearTokenCookieAndRedirect();
        }

        public void AccessDenied()
        {
            RenderView("AccessDenied");
        }

        #region Helper Methods
        private void ClearTokenCookieAndRedirect()
        {
            Response.RemoveCookie("token");
            RedirectToAction("Index");
        }

        private void StoreTokenCookie(string token)
        {
            HttpCookie tokenCookie = new HttpCookie("token", token);
            tokenCookie.Expires = DateTime.Now.AddYears(1);

            Response.CreateCookie(tokenCookie);
        }
        #endregion
    }
}
