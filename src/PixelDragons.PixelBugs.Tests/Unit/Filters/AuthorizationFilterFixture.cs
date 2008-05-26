using System.Collections.Generic;
using Castle.MonoRail.Framework;
using MbUnit.Framework;
using Moq;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Tests.Unit.Controllers;
using PixelDragons.PixelBugs.Web.Controllers;
using PixelDragons.PixelBugs.Web.Filters;

namespace PixelDragons.PixelBugs.Tests.Unit.Filters
{
    [TestFixture]
    public class AuthorizationFilterFixture : ControllerUnitTestBase
    {
        [Test]
        public void Perform_NoPrinciple()
        {
            User user = null;
            
            var contextMock = new Mock<IRailsEngineContext>();
            var issueRepository = new Mock<IIssueRepository>();
            var userRepository = new Mock<IUserRepository>();

            contextMock.Expect(c => c.CurrentUser).Returns(user);

            IssueController controller = new IssueController(issueRepository.Object, userRepository.Object);
            PrepareController(controller, "Issue", "Create");

            AuthorizationFilter filter = new AuthorizationFilter();

            Assert.IsFalse(filter.Perform(ExecuteEnum.BeforeAction, contextMock.Object, controller));
            Assert.AreEqual(@"/Security/AccessDenied.ashx", Response.RedirectedTo, "The filter did not redirect correctly");

            contextMock.VerifyAll();
        }

        [Test]
        public void Perform_PrincipleDoesNotHavePermission()
        {
            var contextMock = new Mock<IRailsEngineContext>();
            var issueRepository = new Mock<IIssueRepository>();
            var userRepository = new Mock<IUserRepository>();

            contextMock.Expect(c => c.CurrentUser).Returns(new User());

            IssueController controller = new IssueController(issueRepository.Object, userRepository.Object);
            PrepareController(controller, "Issue", "Create");

            AuthorizationFilter filter = new AuthorizationFilter();

            Assert.IsFalse(filter.Perform(ExecuteEnum.BeforeAction, contextMock.Object, controller));
            Assert.AreEqual(@"/Security/AccessDenied.ashx", Response.RedirectedTo, "The filter did not redirect correctly");

            contextMock.VerifyAll();
        }

        [Test]
        public void Perform_PrincipleHasPermission()
        {
            User user = new User();
            user.FirstName = "Andy";
            user.LastName = "Pike";

            user.Roles = new List<Role>();
            user.Roles.Add(new Role());
            user.Roles[0].Permissions = new List<Permission>();
            user.Roles[0].Permissions.Add(Permission.CreateIssues);
            
            var contextMock = new Mock<IRailsEngineContext>();
            var issueRepository = new Mock<IIssueRepository>();
            var userRepository = new Mock<IUserRepository>();

            contextMock.Expect(c => c.CurrentUser).Returns(user);

            IssueController controller = new IssueController(issueRepository.Object, userRepository.Object);
            PrepareController(controller, "Issue", "Create");

            AuthorizationFilter filter = new AuthorizationFilter();

            Assert.IsTrue(filter.Perform(ExecuteEnum.BeforeAction, contextMock.Object, controller), "The filter did not allow execution to continue");

            contextMock.VerifyAll();
        }
    }
}
