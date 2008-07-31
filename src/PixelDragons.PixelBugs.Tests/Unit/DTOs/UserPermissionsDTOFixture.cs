using System;
using System.Security.Principal;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;

namespace PixelDragons.PixelBugs.Tests.Unit.DTOs
{
    [TestFixture]
    public class When_the_permission_response_is_created
    {
        private UserPermissionsDTO dto;
        private Guid id;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            List<Permission> permissions = new List<Permission> { Permission.CreateCards };
            dto = new UserPermissionsDTO(id, permissions);
        }

        [Test]
        public void Should_return_the_users_id()
        {
            Assert.That(dto.Id, Is.EqualTo(id));
        }

        [Test]
        public void Should_return_true_when_checking_for_a_permission_that_the_user_has_been_granted()
        {
            Assert.That(dto.Permissions.Contains(Permission.CreateCards), Is.True);
        }

        [Test]
        public void Should_return_false_when_checking_for_a_permission_that_the_user_has_not_been_granted()
        {
            Assert.That(dto.Permissions.Contains(Permission.ViewCards), Is.False);
        }

        [Test]
        public void Should_return_false_when_checking_for_a_permission_if_permissions_are_null()
        {
            dto = new UserPermissionsDTO(id, null);

            Assert.That(dto.Permissions.Contains(Permission.ViewCards), Is.False);
        }

        [Test]
        public void Should_return_an_identity_with_the_name_matching_the_users_id()
        {
            IIdentity identity = dto.Identity;

            Assert.That(identity.Name, Is.EqualTo(dto.Id.ToString()));
        }

        [Test]
        public void Should_always_return_false_when_checking_for_a_system_role()
        {
            //We don't support this method, but here is a test to ensure it doesn't throw if called by the system
            Assert.That(dto.IsInRole("A Role"), Is.False);
        }
    }
}