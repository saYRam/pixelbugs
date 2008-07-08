using System.Security;
using NUnit.Framework;
using Moq;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using PixelDragons.Commons.TestSupport;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class When_trying_to_access_the_application_with_valid_credentials : ControllerUnitTestBase
    {
        SecurityController _controller;
        Mock<ISecurityService> _securityService;
        private const string token = "ABC123";

        [SetUp]
        public void Setup()
        {
            _securityService = new Mock<ISecurityService>();

            _controller = new SecurityController(_securityService.Object);
            PrepareController(_controller, "Security");
        }

        [Test]
        public void Should_render_the_sign_in_view()
        {
            _controller.Index();

            Assert.That(_controller.SelectedViewName, Is.EqualTo(@"Security\Index"), "The wrong view was rendered");
        }

        [Test]
        public void Should_authenticate_the_user_set_a_cookie_with_a_security_token_and_redirect_to_the_card_wall()
        {
            _securityService.Expect(s => s.Authenticate("test.user", "test123")).Returns(token);
            
            _controller.Authenticate("test.user", "test123");

            Assert.That(Cookies["token"].Value, Is.EqualTo(token), "The security token wasn't stored in a cookie");
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Card/Index.ashx"), "The action did not redirect correctly");

            _securityService.VerifyAll();
        }

        [Test]
        public void Should_sign_out_of_the_application_clear_the_security_token_cookie_and_should_redirect_back_to_the_sign_in_view()
        {
            _controller.SignOut();

            Assert.That(Cookies.ContainsKey("token"), Is.False, "The token cookie was not removed correctly");
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Security/Index.ashx"), "The action did not redirect correctly");
        }

    }

    [TestFixture]
    public class When_trying_to_access_the_application_with_invalid_credentials : ControllerUnitTestBase
    {
        SecurityController _controller;
        Mock<ISecurityService> _securityService;

        [SetUp]
        public void Setup()
        {
            _securityService = new Mock<ISecurityService>();

            _controller = new SecurityController(_securityService.Object);
            PrepareController(_controller, "Security");
        }

        [Test]
        public void Should_not_authenticate_the_user_clear_the_security_token_cookie_and_should_redirect_back_to_the_sign_in_view_with_an_error_message()
        {
            _securityService.Expect(s => s.Authenticate("invalid", "credentials")).Throws(new SecurityException());

            _controller.Authenticate("invalid", "credentials");

            Assert.That(Cookies.ContainsKey("token"), Is.False, "The token cookie was not removed correctly");
            Assert.That(_controller.Flash["error"], Is.EqualTo("InvalidCredentials"), "The flash didn't contain the correct error hint");
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Security/Index.ashx"), "The action did not redirect correctly");

            _securityService.VerifyAll();
        }

        [Test]
        public void Should_render_the_access_denied_view()
        {
            _controller.AccessDenied();

            Assert.That(_controller.SelectedViewName, Is.EqualTo(@"Security\AccessDenied"), "The wrong view was rendered");
        }
    }
}
