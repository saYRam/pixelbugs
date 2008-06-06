using System;
using System.Security;
using MbUnit.Framework;
using Moq;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Tests.Unit.Services
{
    [TestFixture]
    public class SimpleSecurityServiceFixture
    {
        SimpleSecurityService _service;
        Mock<IRepository<User>> _userRepositoty;
        Mock<IUserQueries> _userQueries;

        [SetUp]
        public void TestSetup()
        {
            _userRepositoty = new Mock<IRepository<User>>();
            _userQueries = new Mock<IUserQueries>();
            _service = new SimpleSecurityService(_userRepositoty.Object, _userQueries.Object);
        }

        [Test]
        public void Authenticate_Success()
        {
            User[] users = new User[] { new User() };
            users[0].Id = Guid.NewGuid();

            DetachedCriteria criterion = DetachedCriteria.For<User>();

            _userQueries.Expect(q => q.BuildAuthenticationQuery("userName", "password")).Returns(criterion);
            _userRepositoty.Expect(r => r.FindAll(criterion)).Returns(users);

            string token = _service.Authenticate("userName", "password");

            Assert.AreEqual(users[0].Id.ToString(), token);

            _userQueries.VerifyAll();
            _userRepositoty.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(SecurityException))]
        public void Authenticate_InvalidCredentials()
        {
            DetachedCriteria criterion = DetachedCriteria.For<User>();

            _userQueries.Expect(q => q.BuildAuthenticationQuery("invalid", "credentials")).Returns(criterion);
            _userRepositoty.Expect(r => r.FindAll(criterion)).Returns(new User[] {});

            _service.Authenticate("invalid", "credentials");
        }

        [Test]
        public void GetAuthenticatedUserFromToken_Success()
        {
            User user = new User();
            user.Id = Guid.NewGuid();

            _userRepositoty.Expect(r => r.FindById(user.Id)).Returns(user);

            User authenticatedUser = _service.GetAuthenticatedUserFromToken(user.Id.ToString());

            Assert.AreEqual(user, authenticatedUser);
        }

        [Test]
        public void GetAuthenticatedUserFromToken_InvalidToken()
        {
            User authenticatedUser = _service.GetAuthenticatedUserFromToken("invalid token");

            Assert.IsNull(authenticatedUser);
        }
    }
}
