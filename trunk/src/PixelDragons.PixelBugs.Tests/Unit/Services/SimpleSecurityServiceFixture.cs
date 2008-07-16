using System;
using System.Security;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages.SecurityService;
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
        private IQueryBuilder query;

        [SetUp]
        public void TestSetup()
        {
            mockery = new MockRepository();

            userRepositoty = mockery.DynamicMock<IRepository<User>>();
            query = mockery.DynamicMock<IQueryBuilder>();

            service = new SimpleSecurityService(userRepositoty);
        }

        [Test]
        public void Should_create_a_security_token_if_user_credentials_are_valid()
        {
            User[] users = new[] {new User()};
            users[0].Id = Guid.NewGuid();
            AuthenticateUserResponse response;

            using (mockery.Record())
            {
                Expect.Call(userRepositoty.Find(query)).Return(users)
                    .IgnoreArguments();
            }

            using (mockery.Playback())
            {
                response = service.Authenticate(new AuthenticateUserRequest("userName", "password"));
            }

            Assert.That(response.Token, Is.EqualTo(users[0].Id.ToString()));
        }

        [Test]
        [ExpectedException(typeof (SecurityException))]
        public void Should_throw_an_exception_if_user_credentials_are_invalid()
        {
            using (mockery.Record())
            {
                Expect.Call(userRepositoty.Find(query)).Return(new User[] {})
                    .IgnoreArguments();
            }

            using (mockery.Playback())
            {
                service.Authenticate(new AuthenticateUserRequest("invalid", "credentials"));
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidRequestException))]
        public void Should_throw_an_exception_if_request_data_is_invalid()
        {
            service.Authenticate(new AuthenticateUserRequest(null, null));
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