using System;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;

namespace PixelDragons.PixelBugs.Web.Filters
{
    public class AuthenticationFilter : IFilter
    {
        private readonly ISecurityService securityService;

        public AuthenticationFilter(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller, IControllerContext controllerContext)
        {
            string token = context.Request.ReadCookie("token");

            User user = securityService.GetAuthenticatedUserFromToken(token);

            if (user != null)
            {
                context.CurrentUser = user;
                controllerContext.PropertyBag["currentUser"] = user;

                return true;
            }

            context.CurrentUser = null;
            context.Response.Redirect("Security", "AccessDenied");

            return false;
        }
    }
}