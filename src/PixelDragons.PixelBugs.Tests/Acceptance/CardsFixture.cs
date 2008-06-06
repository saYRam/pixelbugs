using System;
using MbUnit.Framework;
using WatiN.Core;
using PixelDragons.Commons.TestSupport;

namespace PixelDragons.PixelBugs.Tests.Acceptance
{
    public class CardsFixture : AcceptanceTestBase
    {
        [Test]
        public void New_CompleteFormAndSubmit_NotSelectingUsers()
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
                browser.SelectList(Find.ByName("card.owner.id")).SelectByValue("0");

                browser.Button(Find.ById("save")).Click();

                Assert.IsTrue(browser.ContainsText(title), "Unable to find the title text confirmation. The html is: {0}", browser.Html);

                security.SignOut(browser);
            }
        }

        [Test]
        public void New_CompleteFormAndSubmit_SelectingUsers()
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
    }
}
