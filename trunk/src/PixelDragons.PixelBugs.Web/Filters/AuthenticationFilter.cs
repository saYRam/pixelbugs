using System;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Messages.Impl;
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
                    RetrieveUserPermissionsResponse response = securityService.RetrieveUserPermissions(new RetrieveUserPermissionsRequest(new Guid(token)));

                    context.CurrentUser = response.User;
                    controllerContext.PropertyBag["currentUser"] = response.User;

                    return true;
                }
                catch(InvalidRequestException)
                {
                    context.CurrentUser = null;
                    context.Response.Redirect("Security", "AccessDenied");

                    return false;
                }
            }

            context.CurrentUser = null;
            context.Response.Redirect("Security", "AccessDenied");

            return false;
        }
    }
}