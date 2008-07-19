using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Tests.Integration;
using WatiN.Core;

namespace PixelDragons.PixelBugs.Tests.Integration
{
    public class When_creating_a_new_card : IntegrationTestBase
    {
        [Test, Category("Integration")]
        public void Should_complete_the_new_card_form_without_selecting_an_owner()
        {
            using (IE browser = SignIn())
            {
                browser.GoTo(BuildUrl("Card", "New"));

                Guid testId = Guid.NewGuid();
                string title = String.Format("Title {0}", testId);

                browser.TextField(Find.ByName("card.title")).TypeText(title);
                browser.SelectList(Find.ByName("card.owner.id")).SelectByValue("");

                browser.Button(Find.ById("save")).Click();

                Assert.That(browser.ContainsText(title), Is.True,
                            "Unable to find the title text confirmation. The HTML is: {0}", browser.Html);

                SignOut(browser);
            }
        }

        [Test, Category("Integration")]
        public void Should_complete_the_new_card_form_including_selecting_an_owner()
        {
            using (IE browser = SignIn())
            {
                browser.GoTo(BuildUrl("Card", "New"));

                Guid testId = Guid.NewGuid();
                string title = String.Format("Title {0}", testId);

                browser.TextField(Find.ByName("card.title")).TypeText(title);
                Option owner = browser.SelectList(Find.ByName("card.owner.id")).Option(Find.ByIndex(1));

                Assert.That(1, Is.EqualTo(owner.Index), "There are no users in this database (can't select the owner");

                owner.Select();

                browser.Button(Find.ById("save")).Click();

                Assert.That(browser.ContainsText(title), Is.True,
                            "Unable to find the title text confirmation. The HTML is: {0}", browser.Html);

                SignOut(browser);
            }
        }
    }

    public class When_editing_a_new_card : IntegrationTestBase
    {
        [Test, Category("Integration")]
        public void Should_view_the_first_card_on_the_wall_then_edit_the_card_title()
        {
            using (IE browser = SignIn())
            {
                GoToCardWallAndClickFirstCard(browser);

                browser.Link(Find.ById("EditLink")).Click();

                string newTitle = "New title " + Guid.NewGuid();
                browser.TextField(Find.ByName("card.title")).Clear();
                browser.TextField(Find.ByName("card.title")).TypeText(newTitle);

                browser.Button(Find.ById("save")).Click();

                Assert.That(browser.ContainsText(newTitle), Is.True,
                            "Unable to find the new title text confirmation. The HTML is: {0}", browser.Html);
            }
        }

        private void GoToCardWallAndClickFirstCard(IE browser)
        {
            browser.GoTo(BuildUrl("Card", "Index"));

            string title = String.Empty;
            foreach (Div div in browser.Divs)
            {
                if (div.ClassName != null && div.ClassName.Contains("card"))
                {
                    title = div.Divs[1].InnerHtml;
                    div.Click();
                    break;
                }
            }

            Assert.That(browser.ContainsText(title), Is.True, "Unable to find the correct page title. The HTML is: {0}", browser.Html);
        }
    }
}