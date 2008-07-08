using System.Configuration;
using Castle.MonoRail.Framework;
using Castle.MonoRail.TestSupport;

namespace PixelDragons.Commons.TestSupport
{
    public abstract class ControllerUnitTestBase : BaseControllerTest
    {
        protected override UrlInfo BuildUrlInfo(string areaName, string controllerName, string actionName)
        {
            string extension = ConfigurationManager.AppSettings["extension"] ?? "rails";

            return new UrlInfo(areaName, controllerName, actionName, "", extension);
        }
    }
}