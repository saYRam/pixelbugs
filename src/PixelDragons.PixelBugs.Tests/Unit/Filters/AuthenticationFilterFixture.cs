using System.Web;
using NUnit.Framework;
using Moq;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using PixelDragons.PixelBugs.Web.Filters;

namespace PixelDragons.PixelBugs.Tests.Unit.Filters
{
    [TestFixture]
    public class When_calling_an_action_that_requires_authentication : FilterUnitTestBase
    {
        Mock<ISecurityService> _securityService;
        private const string token = "ABC123";

        [SetUp]
        public void Setup()
        {
            Mock<ICardService> cardService = new Mock<ICardService>();
            _securityService = new Mock<ISecurityService>();

            _filter = new AuthenticationFilter(_securityService.Object);

            _controller = new CardController(cardService.Object);
            PrepareController(_controller, "Card", "List");
        }

        [Test]
        public void Should_allow_execution_to_continue_if_a_valid_security_token_is_read_from_a_cookie()
        {
            User user = new User();

            _securityService.Expect(s => s.GetAuthenticatedUserFromToken(token)).Returns(user);
            
            Cookies.Add("token", new HttpCookie("token", token));

            Assert.That(ExecuteFilter(), Is.True, "The filter returned false");
            Assert.That(Context.CurrentUser, Is.EqualTo(user), "The current user wasn't stored in the context");
            Assert.That(_controller.PropertyBag["currentUser"], Is.EqualTo(user), "The property bag doesn't contain the current user");
        }

        [Test]
        public void Should_redirect_to_the_access_denied_view_if_there_is_no_valid_security_token_cookie()
        {
            Assert.That(ExecuteFilter(), Is.False, "The filter returned true");
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Security/AccessDenied.ashx"), "The filter did not redirect correctly");
        }
    }
}
