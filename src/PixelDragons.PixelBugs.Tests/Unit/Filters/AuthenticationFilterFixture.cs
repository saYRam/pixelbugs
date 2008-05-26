using System.Collections.Generic;
using Castle.MonoRail.Framework;
using MbUnit.Framework;
using Moq;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Tests.Unit.Controllers;
using PixelDragons.PixelBugs.Web.Controllers;
using PixelDragons.PixelBugs.Web.Filters;
using System;

namespace PixelDragons.PixelBugs.Tests.Unit.Filters
{
    [TestFixture]
    public class AuthenticationFilterFixture : ControllerUnitTestBase
    {
        [Test]
        public void Perform_Success()
        {
            User user = new User();
            user.Id = Guid.NewGuid();
            
            var issueRepository = new Mock<IIssueRepository>();
            var userRepository = new Mock<IUserRepository>();

            userRepository.Expect(r => r.FindById(user.Id)).Returns(user);

            IssueController controller = new IssueController(issueRepository.Object, userRepository.Object);
            PrepareController(controller, "Issue", "List");

            controller.Context.Session["currentUserId"] = user.Id;

            AuthenticationFilter filter = new AuthenticationFilter(userRepository.Object);

            Assert.IsTrue(filter.Perform(ExecuteEnum.BeforeAction, controller.Context, controller));
            Assert.AreEqual(user, controller.PropertyBag["currentUser"], "The property bag doesn't contain the current user");
        }

        [Test]
        public void Perform_EmptySession()
        {
            var issueRepository = new Mock<IIssueRepository>();
            var userRepository = new Mock<IUserRepository>();

            IssueController controller = new IssueController(issueRepository.Object, userRepository.Object);
            PrepareController(controller, "Issue", "List");

            controller.Context.Session["currentUserId"] = null;

            AuthenticationFilter filter = new AuthenticationFilter(userRepository.Object);

            Assert.IsFalse(filter.Perform(ExecuteEnum.BeforeAction, controller.Context, controller));
            Assert.AreEqual(@"/Security/Timeout.ashx", Response.RedirectedTo, "The filter did not redirect correctly");
        }
    }
}
