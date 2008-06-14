using System;
using MbUnit.Framework;
using WatiN.Core;
using PixelDragons.Commons.TestSupport;

namespace PixelDragons.PixelBugs.Tests.Acceptance
{
    public class CardsFixture : AcceptanceTestBase
    {
        [Test]
        public void Should_complete_the_new_card_form_without_selecting_an_owner()
        {
            SecurityFixture security = new SecurityFixture();
            
            using (IE browser = security.SignIn())
            {
                browser.GoTo(BuildUrl("Card", "New"));

                Guid testId = Guid.NewGuid();
                string title = String.Format("Title {0}", testId);
                string description = String.Format("Description {0}", testId);

                browser.TextField(Find.ByName("card.title")).TypeText(title);
                browser.TextField(Find.ByName("card.description")).TypeText(description);
                browser.SelectList(Find.ByName("card.owner.id")).SelectByValue("");

                browser.Button(Find.ById("save")).Click();

                Assert.IsTrue(browser.ContainsText(title), "Unable to find the title text confirmation. The html is: {0}", browser.Html);

                security.SignOut(browser);
            }
        }

        [Test]
        public void Should_complete_the_new_card_form_including_selecting_an_owner()
        {
            SecurityFixture security = new SecurityFixture();

            using (IE browser = security.SignIn())
            {
                browser.GoTo(BuildUrl("Card", "New"));

                Guid testId = Guid.NewGuid();
                string title = String.Format("Title {0}", testId);
                string description = String.Format("Description {0}", testId);

                browser.TextField(Find.ByName("card.title")).TypeText(title);
                browser.TextField(Find.ByName("card.description")).TypeText(description);
                Option owner = browser.SelectList(Find.ByName("card.owner.id")).Option(Find.ByIndex(1));

                Assert.AreEqual(1, owner.Index, "There are no users in this database (can't select the owner");

                owner.Select();

                browser.Button(Find.ById("save")).Click();

                Assert.IsTrue(browser.ContainsText(title), "Unable to find the title text confirmation. The html is: {0}", browser.Html);

                security.SignOut(browser);
            }
        }

        [Test]
        public void Should_click_the_first_card_on_the_wall_and_then_display_its_show_view()
        {
            SecurityFixture security = new SecurityFixture();

            using (IE browser = security.SignIn())
            {
                GoToCardWallAndClickFirstCard(browser);
            }
        }

        [Test]
        public void Should_view_the_first_card_on_the_wall_then_edit_the_card_title()
        {
            SecurityFixture security = new SecurityFixture();

            using (IE browser = security.SignIn())
            {
                GoToCardWallAndClickFirstCard(browser);

                browser.Link(Find.ById("EditLink")).Click();

                string newTitle = "New title " + Guid.NewGuid().ToString();
                browser.TextField(Find.ByName("card.title")).Clear();
                browser.TextField(Find.ByName("card.title")).TypeText(newTitle);

                browser.Button(Find.ById("save")).Click();

                Assert.IsTrue(browser.ContainsText(newTitle), "Unable to find the new title text confirmation. The html is: {0}", browser.Html);
            }
        }

        private void GoToCardWallAndClickFirstCard(IE browser)
        {
            browser.GoTo(BuildUrl("Card", "Index"));

            string title = String.Empty;
            foreach (Div div in browser.Divs)
            {
                if (div.ClassName == "card")
                {
                    title = div.Divs[0].InnerHtml;
                    div.Click();
                    break;
                }
            }

            Assert.IsTrue(browser.ContainsText(title), "Unable to find the correct page title. The html is: {0}", browser.Html);
        }
    }
}
