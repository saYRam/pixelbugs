using System.Web;
using MbUnit.Framework;
using Moq;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using PixelDragons.PixelBugs.Web.Filters;

namespace PixelDragons.PixelBugs.Tests.Unit.Filters
{
    [TestFixture]
    public class AuthenticationFilterFixture : FilterUnitTestBase
    {
        Mock<ISecurityService> _securityService;

        [SetUp]
        public void TestSetup()
        {
            Mock<ICardService> cardService = new Mock<ICardService>();
            _securityService = new Mock<ISecurityService>();

            _filter = new AuthenticationFilter(_securityService.Object);

            _controller = new CardController(cardService.Object);
            PrepareController(_controller, "Card", "List");
        }

        [Test]
        public void Perform_Success()
        {
            string token = "ABC123";
            User user = new User();

            _securityService.Expect(s => s.GetAuthenticatedUserFromToken(token)).Returns(user);
            
            Cookies.Add("token", new HttpCookie("token", token));
                        
            Assert.IsTrue(ExecuteFilter(), "The filter returned false");
            Assert.AreEqual(user, Context.CurrentUser, "The current user wasn't stored in the context");
            Assert.AreEqual(user, _controller.PropertyBag["currentUser"], "The property bag doesn't contain the current user");
        }

        [Test]
        public void Perform_EmptyCookie()
        {
            Assert.IsFalse(ExecuteFilter(), "The filter returned true");
            Assert.AreEqual(@"/Security/AccessDenied.ashx", Response.RedirectedTo, "The filter did not redirect correctly");
        }
    }
}
