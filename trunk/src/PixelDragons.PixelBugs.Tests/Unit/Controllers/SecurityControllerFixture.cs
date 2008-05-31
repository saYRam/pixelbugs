using System.Security;
using MbUnit.Framework;
using Moq;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using PixelDragons.Commons.TestSupport;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class SecurityControllerFixture : ControllerUnitTestBase
    {
        SecurityController _controller;
        Mock<ISecurityService> _securityService;

        [SetUp]
        public void TestSetup()
        {
            _securityService = new Mock<ISecurityService>();

            _controller = new SecurityController(_securityService.Object);
            PrepareController(_controller, "Security");
        }

        [Test]
        public void Index_Success()
        {
            _controller.Index();

            Assert.AreEqual(@"Security\Index", _controller.SelectedViewName, "The wrong view was rendered");
        }

        [Test]
        public void Authenticate_Success()
        {
            string token = "ABC123";

            _securityService.Expect(s => s.Authenticate("test.user", "test123")).Returns(token);
            
            _controller.Authenticate("test.user", "test123");

            Assert.AreEqual(token, Cookies["token"].Value, "The security token wasn't stored in a cookie");
            Assert.AreEqual(@"/Issue/List.ashx", Response.RedirectedTo, "The action did not redirect correctly");

            _securityService.VerifyAll();
        }

        [Test]
        public void Authenticate_InvalidCredentials()
        {
            _securityService.Expect(s => s.Authenticate("invalid", "credentials")).Throws(new SecurityException());

            _controller.Authenticate("invalid", "credentials");

            Assert.IsFalse(Cookies.ContainsKey("token"), "The token cookie was not removed correctly");
            Assert.AreEqual("InvalidCredentials", _controller.Flash["error"], "The flash didn't contain the correct error hint");
            Assert.AreEqual(@"/Security/Index.ashx", Response.RedirectedTo, "The action did not redirect correctly");

            _securityService.VerifyAll();
        }

        [Test]
        public void SignOut_Success()
        {
            _controller.SignOut();

            Assert.IsFalse(Cookies.ContainsKey("token"), "The token cookie was not removed correctly");
            Assert.AreEqual(@"/Security/Index.ashx", Response.RedirectedTo, "The action did not redirect correctly");
        }

        [Test]
        public void AccessDenied_Success()
        {
            _controller.AccessDenied();

            Assert.AreEqual(@"Security\AccessDenied", _controller.SelectedViewName, "The wrong view was rendered");
        }
    }
}
