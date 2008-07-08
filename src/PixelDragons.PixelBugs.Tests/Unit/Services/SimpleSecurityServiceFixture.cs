using System;
using System.Security;
using NUnit.Framework;
using Moq;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Tests.Unit.Services
{
    [TestFixture]
    public class When_authenticating_a_user
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
        public void Should_create_a_security_token_if_user_credentials_are_valid()
        {
            User[] users = new[] { new User() };
            users[0].Id = Guid.NewGuid();

            DetachedCriteria criterion = DetachedCriteria.For<User>();

            _userQueries.Expect(q => q.BuildAuthenticationQuery("userName", "password")).Returns(criterion);
            _userRepositoty.Expect(r => r.FindAll(criterion)).Returns(users);

            string token = _service.Authenticate("userName", "password");

            Assert.That(token, Is.EqualTo(users[0].Id.ToString()));

            _userQueries.VerifyAll();
            _userRepositoty.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(SecurityException))]
        public void Should_throw_an_exception_if_user_credentials_are_invalid()
        {
            DetachedCriteria criterion = DetachedCriteria.For<User>();

            _userQueries.Expect(q => q.BuildAuthenticationQuery("invalid", "credentials")).Returns(criterion);
            _userRepositoty.Expect(r => r.FindAll(criterion)).Returns(new User[] {});

            _service.Authenticate("invalid", "credentials");
        }

        [Test]
        public void Should_retrieve_a_user_from_a_valid_security_token()
        {
            User user = new User {Id = Guid.NewGuid()};

            _userRepositoty.Expect(r => r.FindById(user.Id)).Returns(user);

            User authenticatedUser = _service.GetAuthenticatedUserFromToken(user.Id.ToString());

            Assert.That(authenticatedUser, Is.EqualTo(user));
        }

        [Test]
        public void Should_return_null_for_an_invalid_security_token()
        {
            User authenticatedUser = _service.GetAuthenticatedUserFromToken("invalid token");

            Assert.That(authenticatedUser, Is.Null);
        }
    }
}
