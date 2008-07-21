using System;
using System.Security;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;
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
        private Guid id;

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();
            securityService = mockery.DynamicMock<ISecurityService>();
            id = new Guid();    

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
            AuthenticateRequest request = new AuthenticateRequest("test.user", "test123");
            AuthenticateResponse response = new AuthenticateResponse(id);

            using (mockery.Record())
            {
                Expect.Call(securityService.Authenticate(request)).Return(response);
            }

            using (mockery.Playback())
            {
                controller.Authenticate(request);
            }

            Assert.That(Cookies["token"].Value, Is.EqualTo(id.ToString()));
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Card/Index.ashx"));
        }

        [Test]
        public void Should_sign_out_of_the_application_clear_the_security_token_cookie_and_should_redirect_back_to_the_sign_in_view()
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
        public void Should_clear_the_security_cookie_and_redirect_to_the_sign_in_view_with_an_error_message()
        {
            AuthenticateRequest request = new AuthenticateRequest("invalid", "credentials");

            using (mockery.Record())
            {
                Expect.Call(securityService.Authenticate(request)).Throw(new SecurityException());
            }

            using (mockery.Playback())
            {
                controller.Authenticate(request);
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

        [Test]
        public void Should_clear_the_security_cookie_and_redirect_to_the_sign_in_view_with_an_error_message_when_credentials_are_blank()
        {
            AuthenticateRequest request = new AuthenticateRequest("", "");

            using (mockery.Record())
            {
                Expect.Call(securityService.Authenticate(request)).Throw(new InvalidRequestException("Missing credentials"));
            }

            using (mockery.Playback())
            {
                controller.Authenticate(request);
            }

            Assert.That(Cookies.ContainsKey("token"), Is.False);
            Assert.That(controller.Flash["error"], Is.EqualTo("InvalidCredentials"));
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Security/Index.ashx"));
        }
    }
}