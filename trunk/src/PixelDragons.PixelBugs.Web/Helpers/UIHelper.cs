using System;
using Castle.MonoRail.Framework.Helpers;
using PixelDragons.PixelBugs.Core.DTOs;

namespace PixelDragons.PixelBugs.Web.Helpers
{
    public class UIHelper : AbstractHelper
    {
        public string FormatUser(UserDTO user, string defaultText)
        {
            if (user != null)
                return user.FullName;
            
            return defaultText;
        }

        public string FormatDate(DateTime date)
        {
            return date.ToString("d MMM yyyy");
        }
    }
}