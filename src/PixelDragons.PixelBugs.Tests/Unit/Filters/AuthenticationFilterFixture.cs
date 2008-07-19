using System;
using System.Web;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Messages;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using PixelDragons.PixelBugs.Web.Filters;
using Rhino.Mocks;

namespace PixelDragons.PixelBugs.Tests.Unit.Filters
{
    [TestFixture]
    public class When_calling_an_action_that_requires_authentication : FilterUnitTestBase
    {
        private MockRepository mockery;
        private ISecurityService securityService;
        private ICardService cardService;

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();
            securityService = mockery.DynamicMock<ISecurityService>();
            cardService = mockery.DynamicMock<ICardService>();

            filter = new AuthenticationFilter(securityService);

            controller = new CardController(cardService);
            PrepareController(controller, "Card", "List");
        }

        [Test]
        public void Should_allow_execution_to_continue_if_a_valid_security_token_is_read_from_a_cookie()
        {
            Guid id = Guid.NewGuid();
            RetrieveUserResponse response = new RetrieveUserResponse(id, null, "");
            bool continueExecution;

            Cookies.Add("token", new HttpCookie("token", id.ToString()));

            using (mockery.Record())
            {
                Expect.Call(securityService.RetrieveUser(null)).Return(response)
                    .IgnoreArguments();
            }

            using (mockery.Playback())
            {
                continueExecution = ExecuteFilter();
            }

            Assert.That(continueExecution, Is.True);
            Assert.That(Context.CurrentUser, Is.InstanceOfType(typeof(IRetrievedUser)));
            Assert.That(controller.PropertyBag["currentUser"], Is.EqualTo(Context.CurrentUser));
        }

        [Test]
        public void Should_redirect_to_the_access_denied_view_if_there_is_no_valid_security_token_cookie()
        {
            Assert.That(ExecuteFilter(), Is.False);
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Security/AccessDenied.ashx"));
        }
    }
}