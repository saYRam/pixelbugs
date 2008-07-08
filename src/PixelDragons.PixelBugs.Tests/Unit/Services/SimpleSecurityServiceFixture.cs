using System;
using System.Security;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Queries;
using PixelDragons.PixelBugs.Core.Services;
using Rhino.Mocks;

namespace PixelDragons.PixelBugs.Tests.Unit.Services
{
    [TestFixture]
    public class When_authenticating_a_user
    {
        private MockRepository mockery;
        private SimpleSecurityService service;
        private IRepository<User> userRepositoty;
        private IUserQueries userQueries;

        [SetUp]
        public void TestSetup()
        {
            mockery = new MockRepository();

            userRepositoty = mockery.DynamicMock<IRepository<User>>();
            userQueries = mockery.DynamicMock<IUserQueries>();

            service = new SimpleSecurityService(userRepositoty, userQueries);
        }

        [Test]
        public void Should_create_a_security_token_if_user_credentials_are_valid()
        {
            User[] users = new[] {new User()};
            users[0].Id = Guid.NewGuid();
            string token;

            DetachedCriteria criterion = DetachedCriteria.For<User>();

            using (mockery.Record())
            {
                Expect.Call(userQueries.BuildAuthenticationQuery("userName", "password")).Return(criterion);
                Expect.Call(userRepositoty.FindAll(criterion)).Return(users);
            }

            using (mockery.Playback())
            {
                token = service.Authenticate("userName", "password");
            }

            Assert.That(token, Is.EqualTo(users[0].Id.ToString()));
        }

        [Test]
        [ExpectedException(typeof (SecurityException))]
        public void Should_throw_an_exception_if_user_credentials_are_invalid()
        {
            DetachedCriteria criterion = DetachedCriteria.For<User>();

            using (mockery.Record())
            {
                Expect.Call(userQueries.BuildAuthenticationQuery("invalid", "credentials")).Return(criterion);
                Expect.Call(userRepositoty.FindAll(criterion)).Return(new User[] {});
            }

            using (mockery.Playback())
            {
                service.Authenticate("invalid", "credentials");
            }
        }

        [Test]
        public void Should_retrieve_a_user_from_a_valid_security_token()
        {
            User user = new User {Id = Guid.NewGuid()};
            User authenticatedUser;

            using (mockery.Record())
            {
                Expect.Call(userRepositoty.FindById(user.Id)).Return(user);
            }

            using (mockery.Playback())
            {
                authenticatedUser = service.GetAuthenticatedUserFromToken(user.Id.ToString());
            }

            Assert.That(authenticatedUser, Is.EqualTo(user));
        }

        [Test]
        public void Should_return_null_for_an_invalid_security_token()
        {
            User authenticatedUser = service.GetAuthenticatedUserFromToken("invalid token");

            Assert.That(authenticatedUser, Is.Null);
        }
    }
}