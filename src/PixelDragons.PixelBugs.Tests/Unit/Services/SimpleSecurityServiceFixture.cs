using System;
using System.Security;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages;
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
        private IRetrievedUserPermissionsMapper userPermissionsMapper;

        [SetUp]
        public void TestSetup()
        {
            mockery = new MockRepository();

            userRepositoty = mockery.DynamicMock<IRepository<User>>();
            query = mockery.DynamicMock<IQueryBuilder>();
            userPermissionsMapper = mockery.DynamicMock<IRetrievedUserPermissionsMapper>();

            service = new SimpleSecurityService(userRepositoty, userPermissionsMapper);
        }

        [Test]
        public void Should_return_the_users_id_if_user_credentials_are_valid()
        {
            User[] users = new[] {new User()};
            users[0].Id = Guid.NewGuid();
            AuthenticateResponse response;

            using (mockery.Record())
            {
                Expect.Call(userRepositoty.Find(query)).Return(users)
                    .IgnoreArguments();
            }

            using (mockery.Playback())
            {
                response = service.Authenticate(new AuthenticateRequest("userName", "password"));
            }

            Assert.That(response.Id, Is.EqualTo(users[0].Id));
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
                service.Authenticate(new AuthenticateRequest("invalid", "credentials"));
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidRequestException))]
        public void Should_throw_an_exception_if_request_data_is_invalid()
        {
            service.Authenticate(new AuthenticateRequest(null, null));
        }

    }

    [TestFixture]
    public class When_retrieving_permissions_for_a_user
    {
        private MockRepository mockery;
        private SimpleSecurityService service;
        private IRepository<User> userRepositoty;
        private IRetrievedUserPermissionsMapper userPermissionsMapper;

        [SetUp]
        public void SetUp()
        {
            mockery = new MockRepository();

            userRepositoty = mockery.DynamicMock<IRepository<User>>();
            userPermissionsMapper = mockery.DynamicMock<IRetrievedUserPermissionsMapper>();

            service = new SimpleSecurityService(userRepositoty, userPermissionsMapper);
        }

        [Test]
        public void Should_retrieve_a_users_permissions_from_a_valid_id()
        {
            Guid id = Guid.NewGuid();
            User user = new User {Id = id};
            RetrieveUserPermissionsRequest permissionsRequest = new RetrieveUserPermissionsRequest(id);
            RetrieveUserPermissionsResponse mappedUser = new RetrieveUserPermissionsResponse(id, null);
            RetrieveUserPermissionsResponse response;

            using (mockery.Record())
            {
                Expect.Call(userRepositoty.FindById(permissionsRequest.Id)).Return(user);
                Expect.Call(userPermissionsMapper.MapFrom(user)).Return(mappedUser);
            }

            using (mockery.Playback())
            {
                response = service.RetrieveUserPermissions(permissionsRequest);
            }

            Assert.That(response, Is.EqualTo(mappedUser));
        }

        [Test]
        [ExpectedException(typeof(InvalidRequestException))]
        public void Should_throw_an_exception_for_an_invalid_id()
        {
            RetrieveUserPermissionsRequest permissionsRequest = new RetrieveUserPermissionsRequest(Guid.NewGuid());

            using (mockery.Record())
            {
                Expect.Call(userRepositoty.FindById(permissionsRequest.Id)).Throw(new Exception());
            }

            using (mockery.Playback())
            {
                service.RetrieveUserPermissions(permissionsRequest);
            }
        }
    }
}