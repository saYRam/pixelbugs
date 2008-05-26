using System;
using Castle.MonoRail.Framework;
using System.Reflection;
using PixelDragons.PixelBugs.Core.Attributes;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Web.Filters
{
    public class AuthorizationFilter : IFilter
    {
        public bool Perform(ExecuteEnum exec, IRailsEngineContext context, Controller controller)
        {
            MethodInfo action = controller.MetaDescriptor.Actions[controller.Action] as MethodInfo;
            if (action == null)
            {
                throw new InvalidOperationException("The method info for the currently executing action could not be retrieved.");
            }

            object[] attributes = action.GetCustomAttributes(typeof(PermissionRequiredAttribute), true);

            if (attributes.Length == 0)
            {
                //There are no permission requirements for this action so continue
                return true;
            }
            else
            { 
                //There are permission requirements so get the current user and check their permissions
                User user = context.CurrentUser as User;

                if (user != null)
                {
                    foreach (PermissionRequiredAttribute attribute in attributes)
                    {
                        if (!user.HasPermission(attribute.Permission))
                        {
                            controller.Redirect("Security", "AccessDenied");
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    //There is no user so we can't authorize this action
                    controller.Redirect("Security", "AccessDenied");
                    return false;
                }
            }
        }
    }
}
