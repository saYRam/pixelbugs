using System.Security;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using Rhino.Mocks;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class When_trying_to_access_the_application_with_valid_credentials : ControllerUnitTestBase
    {
        private MockRepository mockery;
        private SecurityController controller;
        private ISecurityService securityService;
        private const string token = "ABC123";

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();
            securityService = mockery.DynamicMock<ISecurityService>();

            controller = new SecurityController(securityService);
            PrepareController(controller, "Security");
        }

        [Test]
        public void Should_render_the_sign_in_view()
        {
            controller.Index();

            Assert.That(controller.SelectedViewName, Is.EqualTo(@"Security\Index"));
        }

        [Test]
        public void Should_authenticate_the_user_set_a_cookie_with_a_security_token_and_redirect_to_the_card_wall()
        {
            using (mockery.Record())
            {
                Expect.Call(securityService.Authenticate("test.user", "test123")).Return(token);
            }

            using (mockery.Playback())
            {
                controller.Authenticate("test.user", "test123");
            }

            Assert.That(Cookies["token"].Value, Is.EqualTo(token));
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Card/Index.ashx"));
        }

        [Test]
        public void
            Should_sign_out_of_the_application_clear_the_security_token_cookie_and_should_redirect_back_to_the_sign_in_view
            ()
        {
            controller.SignOut();

            Assert.That(Cookies.ContainsKey("token"), Is.False);
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Security/Index.ashx"));
        }
    }

    [TestFixture]
    public class When_trying_to_access_the_application_with_invalid_credentials : ControllerUnitTestBase
    {
        private MockRepository mockery;
        private SecurityController controller;
        private ISecurityService securityService;

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();
            securityService = mockery.DynamicMock<ISecurityService>();

            controller = new SecurityController(securityService);
            PrepareController(controller, "Security");
        }

        [Test]
        public void
            Should_not_authenticate_the_user_clear_the_security_token_cookie_and_should_redirect_back_to_the_sign_in_view_with_an_error_message
            ()
        {
            using (mockery.Record())
            {
                Expect.Call(securityService.Authenticate("invalid", "credentials")).Throw(new SecurityException());
            }

            using (mockery.Playback())
            {
                controller.Authenticate("invalid", "credentials");
            }

            Assert.That(Cookies.ContainsKey("token"), Is.False);
            Assert.That(controller.Flash["error"], Is.EqualTo("InvalidCredentials"));
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Security/Index.ashx"));
        }

        [Test]
        public void Should_render_the_access_denied_view()
        {
            controller.AccessDenied();

            Assert.That(controller.SelectedViewName, Is.EqualTo(@"Security\AccessDenied"));
        }
    }
}