using System;
using System.Collections.Generic;
using System.Security.Principal;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Tests.Unit.Domain
{
    [TestFixture]
    public class When_accessing_a_user
    {
        private User user;

        [SetUp]
        public void TestSetup()
        {
            //Setup our test user with no permissions
            user = new User {FirstName = "Andy", LastName = "Pike", Roles = new List<Role> {new Role()}};
            user.Roles[0].Permissions = new List<Permission>();
        }

        [Test]
        public void Should_return_the_correct_fullname()
        {
            Assert.That(user.FullName, Is.EqualTo("Andy Pike"));
        }

        [Test]
        public void Should_return_true_when_checking_for_a_permission_that_the_user_has_been_granted_via_an_enum()
        {
            user.Roles[0].Permissions.Add(Permission.CreateCards);

            Assert.That(user.HasPermission(Permission.CreateCards), Is.True);
        }

        [Test]
        public void Should_return_false_when_checking_for_a_permission_that_the_user_has_not_been_granted_via_an_enum()
        {
            user.Roles[0].Permissions.Add(Permission.ViewCards);

            Assert.That(user.HasPermission(Permission.CreateCards), Is.False);
        }

        [Test]
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
        }

        [Test]
        public void Should_return_an_identity_with_the_name_matching_the_users_full_name()
        {
            IIdentity identity = user.Identity;

            Assert.That(identity.Name, Is.EqualTo(user.FullName));
        }

        [Test]
        public void Should_always_return_false_when_checking_for_a_system_role()
        {
            //We don't support this method, but here is a test to ensure it doesn't fail if called by the system
            Assert.That(user.IsInRole("A Role"), Is.False);
        }
    }
}