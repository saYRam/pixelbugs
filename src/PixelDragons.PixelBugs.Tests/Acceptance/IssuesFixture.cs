using System;
using MbUnit.Framework;
using WatiN.Core;
using PixelDragons.Commons.TestSupport;

namespace PixelDragons.PixelBugs.Tests.Acceptance
{
    public class IssuesFixture : AcceptanceTestBase
    {
        [Test]
        public void New_RenderNewIssuePage()
        {
            SecurityFixture security = new SecurityFixture();
            
            using (IE browser = security.SignIn())
            {
                browser.GoTo(BuildUrl("Issue", "New"));

                //Check the text of the rendered html
                Assert.IsTrue(browser.ContainsText("New Issue"), "New issue page doesn't contain the correct page title. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Summary:"), "New issue page doesn't contain the summary label. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Description:"), "New issue page doesn't contain the description label. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Owned By:"), "New issue page doesn't contain the owned by label. The html is: {0}", browser.Html);

                //Check the form controls are part of the rendered html
                Assert.IsTrue(browser.TextField(Find.ByName("issue.summary")).Exists, "Unable to find the summary text box. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.TextField(Find.ByName("issue.description")).Exists, "Unable to find the description text box. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.SelectList(Find.ByName("issue.ownedBy.id")).Exists, "Unable to find the owned by select list. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.Button(Find.ById("save")).Exists, "Unable to find the save button. The html is: {0}", browser.Html);

                security.SignOut(browser);
            }
        }

        [Test]
        public void New_CompleteFormAndSubmit_NotSelectingUsers()
        {
            SecurityFixture security = new SecurityFixture();
            
            using (IE browser = security.SignIn())
            {
                browser.GoTo(BuildUrl("Issue", "New"));

                Guid testId = Guid.NewGuid();
                string summary = String.Format("Summary {0}", testId);
                string description = String.Format("Description {0}", testId);

                browser.TextField(Find.ByName("issue.summary")).TypeText(summary);
                browser.TextField(Find.ByName("issue.description")).TypeText(description);
                browser.SelectList(Find.ByName("issue.ownedBy.id")).SelectByValue("0");

                browser.Button(Find.ById("save")).Click();

                Assert.IsTrue(browser.ContainsText("Created"), "Unable to find the created date column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Summary"), "Unable to find the summary column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Owner"), "Unable to find the owner column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Reported"), "Unable to find the reported by column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText(summary), "Unable to find the summary text confirmation. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("No Owner"), "Unable to find the No Owner text. The html is: {0}", browser.Html);

                security.SignOut(browser);
            }
        }

        [Test]
        public void New_CompleteFormAndSubmit_SelectingUsers()
        {
            SecurityFixture security = new SecurityFixture();

            using (IE browser = security.SignIn())
            {
                browser.GoTo(BuildUrl("Issue", "New"));

                Guid testId = Guid.NewGuid();
                string summary = String.Format("Summary {0}", testId);
                string description = String.Format("Description {0}", testId);

                browser.TextField(Find.ByName("issue.summary")).TypeText(summary);
                browser.TextField(Find.ByName("issue.description")).TypeText(description);
                Option ownedBy = browser.SelectList(Find.ByName("issue.ownedBy.id")).Option(Find.ByIndex(1));

                Assert.AreEqual(1, ownedBy.Index, "There are no users in this database (can't select the owner");

                string ownedByText = ownedBy.Text;

                ownedBy.Select();

                browser.Button(Find.ById("save")).Click();

                Assert.IsTrue(browser.ContainsText("Created"), "Unable to find the created date column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Summary"), "Unable to find the summary column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Owner"), "Unable to find the owner column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Reported"), "Unable to find the reported by column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText(summary), "Unable to find the summary text confirmation. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText(ownedByText), "Unable to find the owned by user name. The html is: {0}", browser.Html);

                security.SignOut(browser);
            }
        }
    }
}
