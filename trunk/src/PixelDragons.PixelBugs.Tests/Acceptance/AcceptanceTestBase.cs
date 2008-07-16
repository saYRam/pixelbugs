using System;
using Microsoft.VisualStudio.WebHost;
using NUnit.Framework;
using PixelDragons.Commons.TestSupport;
using WatiN.Core;

namespace PixelDragons.PixelBugs.Tests.Acceptance
{
    [TestFixture]
    public abstract class AcceptanceTestBase : TestFixtureBase
    {
        private Server webServer;

        [TestFixtureSetUp]
        public void FixtureSetUp()    
        {
            webServer = new Server(port, "/", physicalPath);
            webServer.Start();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            webServer.Stop();
        }

        protected string BuildUrl(string controller, string action)
        {
            return String.Format("http://{0}:{1}/{2}/{3}.{4}", server, port, controller, action, extension);
        }

        public IE SignIn()
        {
            IE browser = new IE(BuildUrl("Security", "Index"));

            browser.TextField(Find.ByName("request.userName")).TypeText("test.admin");
            browser.TextField(Find.ByName("request.password")).TypeText("password");

            browser.Button(Find.ById("signIn")).Click();

            return browser;
        }

        public void SignOut(IE browser)
        {
            browser.GoTo(BuildUrl("Security", "SignOut"));
        }
    }
}