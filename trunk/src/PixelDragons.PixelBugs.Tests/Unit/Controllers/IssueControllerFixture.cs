using MbUnit.Framework;
using Moq;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Web.Controllers;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class IssueControllerFixture : ControllerUnitTestBase
    {
        [Test]
        public void New_Success()
        { 
            User[] users = new User[] { };

            var issueRepository = new Mock<IIssueRepository>();
            var userRepository = new Mock<IUserRepository>();

            userRepository.Expect(r => r.FindAll()).Returns(users);

            IssueController controller = new IssueController(issueRepository.Object, userRepository.Object); 
            PrepareController(controller, "Issue", "New");

            controller.New();

            Assert.AreEqual(users, controller.PropertyBag["users"], "The property bag doesn't contain the users list");
            Assert.AreEqual(@"Issue\New", controller.SelectedViewName, "The wrong view was rendered");
        }

        [Test]
        public void Create_Success()
        {
            Issue issue = new Issue();
            issue.Summary = "This is a summary";
            issue.Description = "This is a description";

            var issueRepository = new Mock<IIssueRepository>();
            var userRepository = new Mock<IUserRepository>();

            issueRepository.Expect(r => r.Save(issue)).Returns(issue);

            IssueController controller = new IssueController(issueRepository.Object, userRepository.Object);

            PrepareController(controller, "Issue", "Create");

            controller.Create(issue);

            Assert.AreEqual(@"/Issue/List.ashx", Response.RedirectedTo, "The action did not redirect correctly");
        }

        [Test]
        public void List_Success()
        {
            Issue[] issues = new Issue[] { };

            var issueRepository = new Mock<IIssueRepository>();
            var userRepository = new Mock<IUserRepository>();

            issueRepository.Expect(r => r.FindAll()).Returns(issues);

            IssueController controller = new IssueController(issueRepository.Object, userRepository.Object);

            PrepareController(controller, "Issue", "List");

            controller.List();

            Assert.AreEqual(issues, controller.PropertyBag["issues"], "The property bag doesn't contain the issues list");
            Assert.AreEqual(@"Issue\List", controller.SelectedViewName, "The wrong view was rendered");
        }
    }
}
