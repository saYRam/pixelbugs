using System;
using System.Configuration;
using System.Threading;
using MbUnit.Framework;

namespace PixelDragons.PixelBugs.Tests.Acceptance
{
    [TestFixture(ApartmentState = ApartmentState.STA)]
    public class AcceptanceTestBase
    {
        protected string BuildUrl(string controller, string action)
        {
            string server = ConfigurationManager.AppSettings["server"];
            string port = ConfigurationManager.AppSettings["port"];
            string extension = ConfigurationManager.AppSettings["extension"];

            return String.Format("http://{0}:{1}/{2}/{3}.{4}", server, port, controller, action, extension);
        }
    }
}
