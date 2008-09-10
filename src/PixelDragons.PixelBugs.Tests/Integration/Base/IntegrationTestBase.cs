using System;
using System.Configuration;
using Microsoft.VisualStudio.WebHost;
using NUnit.Framework;
using PixelDragons.Commons.TestSupport;
using WatiN.Core;

namespace PixelDragons.PixelBugs.Tests.Integration.Base
{
    [TestFixture]
    public abstract class IntegrationTestBase
    {
        private Server webServer;
        protected string server;
        protected int port;
        protected string extension;
        protected string physicalPath;

        protected IntegrationTestBase()
        {
            server = ConfigurationManager.AppSettings["server"] ?? "localhost";
            extension = ConfigurationManager.AppSettings["extension"] ?? "rails";
            physicalPath = ConfigurationManager.AppSettings["physicalPath"] ?? "";
            port = 80;
            
            if(ConfigurationManager.AppSettings["port"] != null)
            {
                port = int.Parse(ConfigurationManager.AppSettings["port"]);
            }
        }

        [TestFixtureSetUp]
        public void FixtureSetUp()    
        {
            bool startWebServer = bool.Parse(ConfigurationManager.AppSettings["startWebServer"]);

            if (startWebServer)
            {
                webServer = new Server(port, "/", physicalPath);
                webServer.Start();
            }
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            if(webServer != null)
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