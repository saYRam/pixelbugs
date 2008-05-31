using MbUnit.Framework;
using Moq;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class IssueControllerFixture : ControllerUnitTestBase
    {
        IssueController _controller;
        Mock<IIssueService> _issueService;

        [SetUp]
        public void TestSetup()
        {
            _issueService = new Mock<IIssueService>();

            _controller = new IssueController(_issueService.Object);
            PrepareController(_controller, "Issue");
        }

        [Test]
        public void New_Success()
        { 
            User[] users = new User[] { };

            _issueService.Expect(s => s.GetUsersThatCanOwnIssues()).Returns(users);

            _controller.New();

            Assert.AreEqual(users, _controller.PropertyBag["users"], "The property bag doesn't contain the users list");
            Assert.AreEqual(@"Issue\New", _controller.SelectedViewName, "The wrong view was rendered");
        }

        [Test]
        public void Create_Success()
        {
            Issue issue = new Issue();
            User user = new User();

            _issueService.Expect(s => s.SaveIssue(issue, user)).Returns(issue);

            _controller.Create(issue);

            Assert.AreEqual(@"/Issue/List.ashx", Response.RedirectedTo, "The action did not redirect correctly");
        }

        [Test]
        public void List_Success()
        {
            Issue[] issues = new Issue[] { };

            _issueService.Expect(s => s.GetAllIssues()).Returns(issues);

            _controller.List();

            Assert.AreEqual(issues, _controller.PropertyBag["issues"], "The property bag doesn't contain the issues list");
            Assert.AreEqual(@"Issue\List", _controller.SelectedViewName, "The wrong view was rendered");
        }
    }
}
