using System.Collections.Generic;
using MbUnit.Framework;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Tests.Unit.Domain
{
    [TestFixture]
    public class UserFixture
    {
        [Test]
        public void FullName_Success()
        {
            User user = new User();
            user.FirstName = "Andy";
            user.LastName = "Pike";

            Assert.AreEqual("Andy Pike", user.FullName);
        }

        [Test]
        public void HasPermission_ReturnTrue()
        {
            User user = new User();
            user.FirstName = "Andy";
            user.LastName = "Pike";

            user.Roles = new List<Role>();
            user.Roles.Add(new Role());
            user.Roles[0].Permissions = new List<Permission>();
            user.Roles[0].Permissions.Add(Permission.CreateIssues);

            Assert.IsTrue(user.HasPermission(Permission.CreateIssues));
        }

        [Test]
        public void HasPermission_ReturnFalse()
        {
            User user = new User();
            user.FirstName = "Andy";
            user.LastName = "Pike";

            user.Roles = new List<Role>();
            user.Roles.Add(new Role());
            user.Roles[0].Permissions = new List<Permission>();
            user.Roles[0].Permissions.Add(Permission.ViewIssues);

            Assert.IsFalse(user.HasPermission(Permission.CreateIssues));
        }
    }
}
