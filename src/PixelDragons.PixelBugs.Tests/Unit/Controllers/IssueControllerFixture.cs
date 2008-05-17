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
            IssueController controller = new IssueController();
            PrepareController(controller, "Issue", "New");

            controller.New();
            Assert.AreEqual(controller.SelectedViewName, @"Issue\New", "The wrong view was rendered");
        }

        [Test]
        public void Create_Success()
        {
            Issue issue = new Issue();
            issue.Summary = "This is a summary";
            issue.Description = "This is a description";
            
            var mock = new Mock<IIssueRepository>();
            mock.Expect(r => r.Save(issue)).Returns(issue);
            
            IssueController controller = new IssueController();
            controller.IssueRepository = mock.Object;

            PrepareController(controller, "Issue", "Create");

            controller.Create(issue);

            Assert.AreEqual(Response.RedirectedTo, @"/Issue/List.ashx", "The action did not redirect correctly");
        }

        [Test]
        public void List_Success()
        {
            Issue[] issues = new Issue[] { new Issue() };

            var mock = new Mock<IIssueRepository>();
            mock.Expect(r => r.FindAll()).Returns(issues);

            IssueController controller = new IssueController();
            controller.IssueRepository = mock.Object;

            PrepareController(controller, "Issue", "List");

            controller.List();

            Assert.AreEqual(controller.PropertyBag["issues"], issues, "The property bag doesn't contain the issues list");
            Assert.AreEqual(controller.SelectedViewName, @"Issue\List", "The wrong view was rendered");
        }
    }
}
