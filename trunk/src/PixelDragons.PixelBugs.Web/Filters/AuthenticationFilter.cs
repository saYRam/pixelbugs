using System;
using Castle.MonoRail.Framework;
using System.Reflection;
using PixelDragons.PixelBugs.Core.Attributes;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Repositories;

namespace PixelDragons.PixelBugs.Web.Filters
{
    public class AuthenticationFilter : IFilter
    {
        #region Properties
        private IUserRepository UserRepository { get; set; }
        #endregion

        #region Constructors
        public AuthenticationFilter(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        #endregion

        public bool Perform(ExecuteEnum exec, IRailsEngineContext context, Controller controller)
        {
            if (context.Session.Contains("currentUserId") && context.Session["currentUserId"] != null)
            {
                Guid userId = (Guid)context.Session["currentUserId"];

                User user = UserRepository.FindById(userId);

                if (user != null)
                {
                    context.CurrentUser = user;
                    controller.PropertyBag["currentUser"] = user;

                    return true;
                }
            }

            context.CurrentUser = null;
            controller.Redirect("Security", "Timeout");
            return false;
        }
    }
}
