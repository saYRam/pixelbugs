using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages;
using System.Collections.Generic;

namespace PixelDragons.PixelBugs.Tests.Unit.Mappers
{
    [TestFixture]
    public class When_mapping_from_a_user_to_a_retrieved_user_with_permissions
    {
        private IRetrievedUserPermissionsMapper mapper;
        private Role role;
        private Guid id;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            role = new Role {Permissions = new List<Permission>() {Permission.CreateCards}};
            mapper = new RetrievedUserPermissionsMapper();
        }

        [Test]
        public void Should_map_from_a_single_instance()
        {
            User user = new User{Id = id, Roles = new List<Role>() { role } };

            RetrieveUserPermissionsResponse response = mapper.MapFrom(user);

            Assert.That(response.Id, Is.EqualTo(user.Id));
            Assert.That(response.HasPermission(Permission.CreateCards), Is.True);
        }

        [Test]
        public void Should_map_from_a_collection()
        {
            User[] users = new[]
                               {
                                   new User{Id = id, Roles = new List<Role>() {role} },
                                   new User{Id = id, Roles = new List<Role>() {role} }
                               };

            IEnumerable<RetrieveUserPermissionsResponse> mappedUsers = mapper.MapCollection(users);

            foreach (RetrieveUserPermissionsResponse mappedUser in mappedUsers)
            {
                Assert.That(mappedUser.Id, Is.EqualTo(id));
                Assert.That(mappedUser.HasPermission(Permission.CreateCards), Is.True);
            }
        }
    }
}