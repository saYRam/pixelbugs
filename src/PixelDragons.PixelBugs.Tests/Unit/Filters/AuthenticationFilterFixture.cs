using System.Web;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
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
        private ISecurityService _securityService;
        private ICardService cardService;
        private const string token = "ABC123";

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();
            _securityService = mockery.DynamicMock<ISecurityService>();
            cardService = mockery.DynamicMock<ICardService>();

            _filter = new AuthenticationFilter(_securityService);

            _controller = new CardController(cardService);
            PrepareController(_controller, "Card", "List");
        }

        [Test]
        public void Should_allow_execution_to_continue_if_a_valid_security_token_is_read_from_a_cookie()
        {
            User user = new User();
            bool continueExecution;

            Cookies.Add("token", new HttpCookie("token", token));

            using (mockery.Record())
            {
                Expect.Call(_securityService.GetAuthenticatedUserFromToken(token)).Return(user);
            }

            using (mockery.Playback())
            {
                continueExecution = ExecuteFilter();
            }

            Assert.That(continueExecution, Is.True);
            Assert.That(Context.CurrentUser, Is.EqualTo(user));
            Assert.That(_controller.PropertyBag["currentUser"], Is.EqualTo(user));
        }

        [Test]
        public void Should_redirect_to_the_access_denied_view_if_there_is_no_valid_security_token_cookie()
        {
            Assert.That(ExecuteFilter(), Is.False);
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Security/AccessDenied.ashx"));
        }
    }
}
