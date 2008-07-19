using System;
using System.Collections.Generic;
using System.Security.Principal;
using NUnit.Framework;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Messages;
using NUnit.Framework.SyntaxHelpers;

namespace PixelDragons.PixelBugs.Tests.Unit.Messages
{
    [TestFixture]
    public class When_accessing_a_retrieved_user
    {
        private RetrieveUserResponse response;

        [SetUp]
        public void SetUp()
        {
            List<Permission> permissions = new List<Permission>() {Permission.CreateCards};
            response = new RetrieveUserResponse(Guid.NewGuid(), permissions, "Andy Pike");
        }

        [Test]
        public void Should_return_true_when_checking_for_a_permission_that_the_user_has_been_granted()
        {
            Assert.That(response.HasPermission(Permission.CreateCards), Is.True);
        }

        [Test]
        public void Should_return_false_when_checking_for_a_permission_that_the_user_has_not_been_granted()
        {
            Assert.That(response.HasPermission(Permission.ViewCards), Is.False);
        }

        [Test]
        public void Should_return_false_when_checking_for_a_permission_if_permissions_are_null()
        {
            response = new RetrieveUserResponse(Guid.NewGuid(), null, "");

            Assert.That(response.HasPermission(Permission.ViewCards), Is.False);
        }

        [Test]
        public void Should_return_an_identity_with_the_name_matching_the_users_full_name()
        {
            IIdentity identity = response.Identity;

            Assert.That(identity.Name, Is.EqualTo(response.FullName));
        }

        [Test]
        public void Should_always_return_false_when_checking_for_a_system_role()
        {
            //We don't support this method, but here is a test to ensure it doesn't throw if called by the system
            Assert.That(response.IsInRole("A Role"), Is.False);
        }

        /*Needs to go in the new security block view component when it's been created*/
        /*[Test]
        public void Should_return_true_when_checking_for_a_permission_that_the_user_has_been_granted_via_a_string()
        {
            user.Roles[0].Permissions.Add(Permission.CreateCards);

            Assert.That(user.HasPermission("CreateCards"), Is.True);
        }

        [Test]
        public void Should_return_false_when_checking_for_a_permission_that_the_user_has_not_been_granted_via_a_string()
        {
            user.Roles[0].Permissions.Add(Permission.ViewCards);

            Assert.That(user.HasPermission("CreateCards"), Is.False);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void Should_throw_an_exception_if_an_invalid_permission_is_passed_when_checking_permissions()
        {
            user.Roles[0].Permissions.Add(Permission.ViewCards);

            user.HasPermission("This is not a valid permission"); //This should throw an ArgumentException
        }*/
    }
}