using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using PixelDragons.PixelBugs.Web.Filters;
using System;

namespace PixelDragons.PixelBugs.Tests.Unit.Filters
{
    [TestFixture]
    public class When_calling_an_action_that_requires_authorization : FilterUnitTestBase
    {
        [SetUp]
        public void TestSetup()
        {
            Mock<ICardService> cardService = new Mock<ICardService>();
            
            _filter = new AuthorizationFilter();

            _controller = new CardController(cardService.Object);
            PrepareController(_controller, "Card", "New");
        }

        [Test]
        public void Should_redirect_to_the_access_denied_view_if_there_is_no_principle()
        {
            Assert.That(ExecuteFilter(), Is.False);
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Security/AccessDenied.ashx"), "The filter did not redirect correctly");
        }

        [Test]
        public void Should_redirect_to_the_access_denied_view_if_the_principle_does_not_have_the_correct_permission()
        {
            _controller.Context.CurrentUser = new User();

            Assert.That(ExecuteFilter(), Is.False);
            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Security/AccessDenied.ashx"), "The filter did not redirect correctly");
        }

        [Test]
        public void Should_allow_execution_to_continue_if_the_principle_has_the_correct_permissions()
        {
            User user = new User {FirstName = "Andy", LastName = "Pike", Roles = new List<Role> {new Role()}};
            user.Roles[0].Permissions = new List<Permission> {Permission.CreateCards};

            _controller.Context.CurrentUser = user;

            Assert.That(ExecuteFilter(), Is.True, "The filter did not allow execution to continue");
        }

        [Test]
        public void Should_allow_execution_to_continue_if_the_action_has_no_permission_requirements()
        {
            Mock<ISecurityService> securityService = new Mock<ISecurityService>();
            _controller = new SecurityController(securityService.Object);
            PrepareController(_controller, "Security", "Index");

            Assert.That(ExecuteFilter(), Is.True, "The filter did not allow execution to continue");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Should_throw_an_exception_if_the_action_information_cannot_be_retrieved()
        {
            Mock<ISecurityService> securityService = new Mock<ISecurityService>();
            _controller = new SecurityController(securityService.Object);
            PrepareController(_controller, "Security");

            ExecuteFilter();    //This should throw an InvalidOperationException
        }
    }
}
