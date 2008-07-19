using System;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Messages;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Core.Exceptions;

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

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    RetrieveUserResponse response = securityService.RetrieveUser(new RetrieveUserRequest(new Guid(token)));

                    context.CurrentUser = response;
                    controllerContext.PropertyBag["currentUser"] = response;

                    return true;
                }
                catch(InvalidRequestException)
                {
                    context.CurrentUser = null;
                    context.Response.Redirect("Security", "AccessDenied");
                }
            }

            context.CurrentUser = null;
            context.Response.Redirect("Security", "AccessDenied");

            return false;
        }
    }
}