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
        private UIHelper helper;

        [SetUp]
        public void Setup()
        {
            helper = new UIHelper();
        }

        [Test]
        public void Should_return_default_message_if_a_null_user_is_passed()
        {
            string text = helper.FormatUser(null, "No Owner");

            Assert.That(text, Is.EqualTo("No Owner"));
        }

        [Test]
        public void Should_return_the_users_full_name_if_a_valid_user_is_passed()
        {
            User user = new User {FirstName = "Andy", LastName = "Pike"};

            string text = helper.FormatUser(user, "No Owner");

            Assert.That(text, Is.EqualTo("Andy Pike"));
        }
    }

    [TestFixture]
    public class When_formatting_a_date
    {
        private UIHelper helper;

        [SetUp]
        public void Setup()
        {
            helper = new UIHelper();
        }

        [Test]
        public void Should_return_the_date_in_non_locale_specific_format()
        {
            DateTime date = DateTime.Now;

            string formattedDate = helper.FormatDate(date);

            Assert.That(formattedDate, Is.EqualTo(date.ToString("d MMM yyyy")));
        }
    }
}