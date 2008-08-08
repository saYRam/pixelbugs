using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Mappers.Impl;

namespace PixelDragons.PixelBugs.Tests.Unit.Mappers
{
    [TestFixture]
    public class When_mapping_from_a_domain_user_to_a_dto_user_with_permissions
    {
        private IUserPermissionsDTOMapper mapper;
        private Role role;
        private Guid id;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            role = new Role {Permissions = new List<Permission> {Permission.CreateCards}};
            mapper = new UserPermissionsDTOMapper();
        }

        [Test]
        public void Should_map_from_a_single_instance()
        {
            User user = new User{Id = id, Roles = new List<Role> { role } };

            UserPermissionsDTO dto = mapper.MapFrom(user);

            Assert.That(dto.Id, Is.EqualTo(user.Id));
            Assert.That(dto.Permissions.Contains(Permission.CreateCards), Is.True);
        }

        [Test]
        public void Should_map_from_a_collection()
        {
            User[] users = new[]
                               {
                                   new User{Id = id, Roles = new List<Role>() {role} },
                                   new User{Id = id, Roles = new List<Role>() {role} }
                               };

            IEnumerable<UserPermissionsDTO> dtos = mapper.MapCollection(users);

            foreach (UserPermissionsDTO dto in dtos)
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.Permissions.Contains(Permission.CreateCards), Is.True);
            }
        }
    }
}