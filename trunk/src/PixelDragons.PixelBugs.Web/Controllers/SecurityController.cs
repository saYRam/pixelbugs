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
        private readonly ISecurityService securityService;

        public SecurityController(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        public void Index()
        {
            RenderView("Index");
        }

        public void Authenticate(string userName, string password)
        {
            try
            {
                string token = securityService.Authenticate(userName, password);

                StoreTokenCookie(token);

                Redirect("Card", "Index");
            }
            catch (SecurityException)
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

        private void ClearTokenCookieAndRedirect()
        {
            Response.RemoveCookie("token");
            RedirectToAction("Index");
        }

        private void StoreTokenCookie(string token)
        {
            HttpCookie tokenCookie = new HttpCookie("token", token) { Expires = DateTime.Now.AddYears(1) };

            Response.CreateCookie(tokenCookie);
        }
    }
}