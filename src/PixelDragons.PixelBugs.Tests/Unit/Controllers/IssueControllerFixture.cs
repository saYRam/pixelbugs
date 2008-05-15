using Castle.MonoRail.TestSupport;
using MbUnit.Framework;
using PixelDragons.PixelBugs.Web.Controllers;
using Moq;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Repositories;
using Castle.MonoRail.Framework;
using System.IO;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class IssueControllerFixture : BaseControllerTest
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

        protected override UrlInfo BuildUrlInfo(string areaName, string controllerName, string actionName)
        {
            return new UrlInfo("app.com", "www", virtualDir, "http", 80,
                Path.Combine(Path.Combine(areaName, controllerName), actionName), areaName, controllerName, actionName, "ashx");
        }
    }
}
