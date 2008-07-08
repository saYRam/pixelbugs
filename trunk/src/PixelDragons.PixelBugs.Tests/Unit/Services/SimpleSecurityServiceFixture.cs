using System;
using System.Security;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Queries;
using Rhino.Mocks;

namespace PixelDragons.PixelBugs.Tests.Unit.Services
{
    [TestFixture]
    public class When_authenticating_a_user
    {
        private MockRepository mockery;
        private SimpleSecurityService _service;
        private IRepository<User> _userRepositoty;
        private IUserQueries _userQueries;

        [SetUp]
        public void TestSetup()
        {
            mockery = new MockRepository();

            _userRepositoty = mockery.DynamicMock<IRepository<User>>();
            _userQueries = mockery.DynamicMock<IUserQueries>();

            _service = new SimpleSecurityService(_userRepositoty, _userQueries);
        }

        [Test]
        public void Should_create_a_security_token_if_user_credentials_are_valid()
        {
            User[] users = new[] { new User() };
            users[0].Id = Guid.NewGuid();
            string token;

            DetachedCriteria criterion = DetachedCriteria.For<User>();

            using (mockery.Record())
            {
                Expect.Call(_userQueries.BuildAuthenticationQuery("userName", "password")).Return(criterion);
                Expect.Call(_userRepositoty.FindAll(criterion)).Return(users);
            }

            using (mockery.Playback())
            {
                token = _service.Authenticate("userName", "password");
            }

            Assert.That(token, Is.EqualTo(users[0].Id.ToString()));
        }

        [Test]
        [ExpectedException(typeof(SecurityException))]
        public void Should_throw_an_exception_if_user_credentials_are_invalid()
        {
            DetachedCriteria criterion = DetachedCriteria.For<User>();

            using (mockery.Record())
            {
                Expect.Call(_userQueries.BuildAuthenticationQuery("invalid", "credentials")).Return(criterion);
                Expect.Call(_userRepositoty.FindAll(criterion)).Return(new User[] { });
            }

            using (mockery.Playback())
            {
                _service.Authenticate("invalid", "credentials");
            }
        }

        [Test]
        public void Should_retrieve_a_user_from_a_valid_security_token()
        {
            User user = new User {Id = Guid.NewGuid()};
            User authenticatedUser;

            using (mockery.Record())
            {
                Expect.Call(_userRepositoty.FindById(user.Id)).Return(user);
            }

            using (mockery.Playback())
            {
                authenticatedUser = _service.GetAuthenticatedUserFromToken(user.Id.ToString());
            }
            
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
