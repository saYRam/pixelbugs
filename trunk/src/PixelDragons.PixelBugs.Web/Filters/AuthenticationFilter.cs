using System;
using Castle.MonoRail.Framework;
using System.Reflection;
using PixelDragons.PixelBugs.Core.Attributes;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;

namespace PixelDragons.PixelBugs.Web.Filters
{
    public class AuthenticationFilter : IFilter
    {
        #region Properties
        private ISecurityService _securityService;
        #endregion

        #region Constructors
        public AuthenticationFilter(ISecurityService securityService)
        {
            _securityService = securityService;
        }
        #endregion

        public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller, IControllerContext controllerContext)
        {
            string token = context.Request.ReadCookie("token");

            User user = _securityService.GetAuthenticatedUserFromToken(token);

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
