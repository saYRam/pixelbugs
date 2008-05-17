using System.Configuration;
using System.IO;
using Castle.MonoRail.Framework;
using Castle.MonoRail.TestSupport;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    public class ControllerUnitTestBase : BaseControllerTest
    {
        protected override UrlInfo BuildUrlInfo(string areaName, string controllerName, string actionName)
        {
            string extension = ConfigurationManager.AppSettings["extension"];

            return new UrlInfo("app.com", "www", virtualDir, "http", 80,
                Path.Combine(Path.Combine(areaName, controllerName), actionName), areaName, controllerName, actionName, extension);
        }
    }
}
