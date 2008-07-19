using System;
using System.Security;
using System.Web;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;
using PixelDragons.PixelBugs.Core.Services;

namespace PixelDragons.PixelBugs.Web.Controllers
{
    [Layout("Default"), Rescue("GeneralError")]
    [Resource("strings", "PixelDragons.PixelBugs.Web.Resources.Controllers.SecurityController")]
    public class SecurityController : SmartDispatcherController
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

        public void Authenticate([DataBind("request")]AuthenticateRequest request)
        {
            try
            {
                AuthenticateResponse response = securityService.Authenticate(request);

                StoreIdCookie(response.Id);

                Redirect("Card", "Index");
            }
            catch (SecurityException)
            {
                Flash["error"] = "InvalidCredentials";
                ClearTokenCookieAndRedirect();
            }
            catch(InvalidRequestException)
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

        private void StoreIdCookie(Guid id)
        {
            HttpCookie tokenCookie = new HttpCookie("token", id.ToString()) { Expires = DateTime.Now.AddYears(1) };

            Response.CreateCookie(tokenCookie);
        }
    }
}