using System;
using MbUnit.Framework;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Web.Helpers;

namespace PixelDragons.PixelBugs.Tests.Unit.Helpers
{
    [TestFixture]
    public class UIHelperFixture
    {
        [Test]
        public void FormatUser_PassedNull()
        {
            UIHelper helper = new UIHelper();

            string text = helper.FormatUser(null, "Unassigned");

            Assert.AreEqual("Unassigned", text);
        }

        [Test]
        public void FormatUser_PassedUser()
        {
            UIHelper helper = new UIHelper();

            User user = new User();
            user.FirstName = "Andy";
            user.LastName = "Pike";

            string text = helper.FormatUser(user, "Unassigned");

            Assert.AreEqual("Andy Pike", text);
        }

        [Test]
        public void FormatDate_Success()
        {
            UIHelper helper = new UIHelper();

            DateTime date = DateTime.Now;

            string text = helper.FormatDate(date);

            Assert.AreEqual(date.ToString("d MMM yyyy"), text);
        }
    }
}
