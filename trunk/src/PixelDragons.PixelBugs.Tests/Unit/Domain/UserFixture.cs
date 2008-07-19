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
    }
}