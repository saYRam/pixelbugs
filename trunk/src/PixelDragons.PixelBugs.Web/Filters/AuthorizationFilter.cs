using System;
using System.Reflection;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Attributes;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Web.Filters
{
    public class AuthorizationFilter : IFilter
    {
        public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller, IControllerContext controllerContext)
        {  
            MethodInfo action = controllerContext.ControllerDescriptor.Actions[controllerContext.Action] as MethodInfo;
            if (action == null)
                throw new InvalidOperationException("The method info for the currently executing action could not be retrieved.");

            object[] attributes = action.GetCustomAttributes(typeof (PermissionRequiredAttribute), true);

            if (attributes.Length == 0)
            {
                //There are no permission requirements for this action so continue
                return true;
            }
            
            //There are permission requirements so get the current user and check their permissions
            IRetrievedUser user = context.CurrentUser as IRetrievedUser;

            if (user != null)
            {
                foreach (PermissionRequiredAttribute attribute in attributes)
                {
                    if (!user.HasPermission(attribute.Permission))
                    {
                        context.Response.Redirect("Security", "AccessDenied");
                        return false;
                    }
                }

                return true;
            }
            
            //There is no user so we can't authorize this action
            context.Response.Redirect("Security", "AccessDenied");
            return false;
        }
    }
}