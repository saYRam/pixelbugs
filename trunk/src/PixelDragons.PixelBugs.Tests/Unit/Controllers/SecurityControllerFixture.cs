using System;
using MbUnit.Framework;
using Moq;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Web.Controllers;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class SecurityControllerFixture : ControllerUnitTestBase
    {
        [Test]
        public void Index_Success()
        {
            var userRepository = new Mock<IUserRepository>();

            SecurityController controller = new SecurityController(userRepository.Object);
            PrepareController(controller, "Security", "Index");

            controller.Index();

            Assert.AreEqual(@"Security\Index", controller.SelectedViewName, "The wrong view was rendered");
        }

        [Test]
        public void Authenticate_Success()
        {
            User user = new User();
            user.Id = Guid.NewGuid();

            var userRepository = new Mock<IUserRepository>();

            userRepository.Expect(r => r.FindByUserNameAndPassword("test.user", "test123")).Returns(user);

            SecurityController controller = new SecurityController(userRepository.Object);
            PrepareController(controller, "Security", "Authenticate");

            controller.Authenticate("test.user", "test123");

            Assert.AreEqual(user.Id, Context.Session["currentUserId"], "The authenticated user id was not stored in the session");
            Assert.AreEqual(@"/Issue/List.ashx", Response.RedirectedTo, "The action did not redirect correctly");

            userRepository.VerifyAll();
        }

        [Test]
        public void Authenticate_InvalidCredentials()
        {
            User user = null;

            var userRepository = new Mock<IUserRepository>();

            userRepository.Expect(r => r.FindByUserNameAndPassword("invalid", "credentials")).Returns(user);

            SecurityController controller = new SecurityController(userRepository.Object);
            PrepareController(controller, "Security", "Authenticate");

            controller.Authenticate("invalid", "credentials");

            Assert.IsNull(Context.Session["currentUserId"], "The session was not cleared correctly");
            Assert.AreEqual("InvalidCredentials", controller.Flash["error"], "The flash didn't contain the correct error hint");
            Assert.AreEqual(@"/Security/Index.ashx", Response.RedirectedTo, "The action did not redirect correctly");

            userRepository.VerifyAll();
        }
    }
}
