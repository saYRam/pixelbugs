using System;
using MbUnit.Framework;
using Moq;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Core.Services;

namespace PixelDragons.PixelBugs.Tests.Unit.Services
{
    [TestFixture]
    public class IssueServiceFixture
    {
        IssueService _service;
        Mock<IIssueRepository> _issueRepositoty;
        Mock<IUserRepository> _userRepositoty;

        [SetUp]
        public void TestSetup()
        {
            _userRepositoty = new Mock<IUserRepository>();
            _issueRepositoty = new Mock<IIssueRepository>();
            _service = new IssueService(_userRepositoty.Object, _issueRepositoty.Object);
        }

        [Test]
        public void GetAllIssues_Success()
        {
            Issue[] issues = new Issue[] { };
            _issueRepositoty.Expect(r => r.FindAll()).Returns(issues);

            Assert.AreEqual(issues, _service.GetAllIssues());
        }

        [Test]
        public void GetUsersThatCanOwnIssues_Success()
        {
            User[] users = new User[] { };
            _userRepositoty.Expect(r => r.FindAll()).Returns(users);

            Assert.AreEqual(users, _service.GetUsersThatCanOwnIssues());
        }

        [Test]
        public void SaveIssue_NewIssue()
        {
            Issue issue = new Issue();
            User user = new User();

            _issueRepositoty.Expect(r => r.Save(issue)).Returns(issue);

            Issue savedIssue = _service.SaveIssue(issue, user);

            Assert.AreEqual(user, issue.CreatedBy);
            Assert.AreEqual(DateTime.Now.Date, issue.CreatedDate.Date);
        }
    }
}
