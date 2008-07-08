using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Web.Helpers;

namespace PixelDragons.PixelBugs.Tests.Unit.Helpers
{
    [TestFixture]
    public class When_formatting_a_user_name
    {
        UIHelper _helper;

        [SetUp]
        public void TestSetup()
        {
            _helper = new UIHelper();
        }

        [Test]
        public void Should_return_default_message_if_a_null_user_is_passed()
        {
            string text = _helper.FormatUser(null, "No Owner");

            Assert.That(text, Is.EqualTo("No Owner"));
        }

        [Test]
        public void Should_return_the_users_full_name_if_a_valid_user_is_passed()
        {
            User user = new User {FirstName = "Andy", LastName = "Pike"};

            string text = _helper.FormatUser(user, "No Owner");

            Assert.That(text, Is.EqualTo("Andy Pike"));
        }

    }

    [TestFixture]
    public class When_formatting_a_date
    {
        UIHelper _helper;

        [SetUp]
        public void TestSetup()
        {
            _helper = new UIHelper();
        }
        [Test]
        public void Should_return_the_date_in_non_locale_specific_format()
        {
            DateTime date = DateTime.Now;

            string formattedDate = _helper.FormatDate(date);

            Assert.That(formattedDate, Is.EqualTo(date.ToString("d MMM yyyy")));
        }
    }
}
