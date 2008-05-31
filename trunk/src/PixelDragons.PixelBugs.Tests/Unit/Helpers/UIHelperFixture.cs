using System;
using MbUnit.Framework;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Web.Helpers;

namespace PixelDragons.PixelBugs.Tests.Unit.Helpers
{
    [TestFixture]
    public class UIHelperFixture
    {
        UIHelper _helper;

        [SetUp]
        public void TestSetup()
        {
            _helper = new UIHelper();
        }

        [Test]
        public void FormatUser_PassedNull()
        {
            string text = _helper.FormatUser(null, "No Owner");

            Assert.AreEqual("No Owner", text);
        }

        [Test]
        public void FormatUser_PassedUser()
        {
            User user = new User();
            user.FirstName = "Andy";
            user.LastName = "Pike";

            string text = _helper.FormatUser(user, "No Owner");

            Assert.AreEqual("Andy Pike", text);
        }

        [Test]
        public void FormatDate_Success()
        {
            DateTime date = DateTime.Now;

            string text = _helper.FormatDate(date);

            Assert.AreEqual(date.ToString("d MMM yyyy"), text);
        }
    }
}
