using System.Collections.Generic;
using MbUnit.Framework;
using Moq;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using PixelDragons.PixelBugs.Web.Filters;
using System;

namespace PixelDragons.PixelBugs.Tests.Unit.Filters
{
    [TestFixture]
    public class AuthorizationFilterFixture : FilterUnitTestBase
    {
        [SetUp]
        public void TestSetup()
        {
            Mock<IIssueService> issueService = new Mock<IIssueService>();
            
            _filter = new AuthorizationFilter();

            _controller = new IssueController(issueService.Object);
            PrepareController(_controller, "Issue", "New");
        }

        [Test]
        public void Perform_NoPrinciple()
        {
            Assert.IsFalse(ExecuteFilter());
            Assert.AreEqual(@"/Security/AccessDenied.ashx", Response.RedirectedTo, "The filter did not redirect correctly");
        }

        [Test]
        public void Perform_PrincipleDoesNotHavePermission()
        {
            _controller.Context.CurrentUser = new User();

            Assert.IsFalse(ExecuteFilter());
            Assert.AreEqual(@"/Security/AccessDenied.ashx", Response.RedirectedTo, "The filter did not redirect correctly");
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

            _controller.Context.CurrentUser = user;

            Assert.IsTrue(ExecuteFilter(), "The filter did not allow execution to continue");
        }

        [Test]
        public void Perform_ActionHasNoPermissionRequirements_AndNoPrinciple()
        {
            Mock<ISecurityService> securityService = new Mock<ISecurityService>();
            _controller = new SecurityController(securityService.Object);
            PrepareController(_controller, "Security", "Index");

            Assert.IsTrue(ExecuteFilter(), "The filter did not allow execution to continue");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Perform_NoActionInformation()
        {
            Mock<ISecurityService> securityService = new Mock<ISecurityService>();
            _controller = new SecurityController(securityService.Object);
            PrepareController(_controller, "Security");

            ExecuteFilter();    //This should throw an InvalidOperationException
        }
    }
}
