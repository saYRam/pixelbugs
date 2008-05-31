using System.Collections.Generic;
using MbUnit.Framework;
using PixelDragons.PixelBugs.Core.Domain;
using System;
using System.Security.Principal;

namespace PixelDragons.PixelBugs.Tests.Unit.Domain
{
    [TestFixture]
    public class UserFixture
    {
        User _user;

        [SetUp]
        public void TestSetup()
        {
            //Setup our test user with no permissions
            _user = new User();
            _user.FirstName = "Andy";
            _user.LastName = "Pike";

            _user.Roles = new List<Role>();
            _user.Roles.Add(new Role());
            _user.Roles[0].Permissions = new List<Permission>();
        }

        [Test]
        public void FullName_Success()
        {
            Assert.AreEqual("Andy Pike", _user.FullName);
        }

        [Test]
        public void HasPermission_WithEnum_ReturnTrue()
        {
            _user.Roles[0].Permissions.Add(Permission.CreateIssues);

            Assert.IsTrue(_user.HasPermission(Permission.CreateIssues));
        }

        [Test]
        public void HasPermission_WithEnum_ReturnFalse()
        {
            _user.Roles[0].Permissions.Add(Permission.ViewIssues);

            Assert.IsFalse(_user.HasPermission(Permission.CreateIssues));
        }

        [Test]
        public void HasPermission_WithString_ReturnTrue()
        {
            _user.Roles[0].Permissions.Add(Permission.CreateIssues);

            Assert.IsTrue(_user.HasPermission("CreateIssues"));
        }

        [Test]
        public void HasPermission_WithString_ReturnFalse()
        {
            _user.Roles[0].Permissions.Add(Permission.ViewIssues);

            Assert.IsFalse(_user.HasPermission("CreateIssues"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void HasPermission_WithString_InvalidArgument()
        {
            _user.Roles[0].Permissions.Add(Permission.ViewIssues);

            _user.HasPermission("This is not a valid permission");  //This should throw an ArgumentException
        }

        [Test]
        public void Identity_Success()
        {
            IIdentity identity = _user.Identity;

            Assert.AreEqual(_user.FullName, identity.Name);
        }

        [Test]
        public void IsInRole()
        {
            //We don't support this method, but here is a test to ensure it doesn't fail if called by the system
            Assert.IsFalse(_user.IsInRole("A Role"));
        }
    }
}
