using System;
using MbUnit.Framework;
using WatiN.Core;

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
                Assert.IsTrue(browser.ContainsText("Assigned To:"), "New issue page doesn't contain the assigned to label. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Reported By:"), "New issue page doesn't contain the reported by label. The html is: {0}", browser.Html);

                //Check the form controls are part of the rendered html
                Assert.IsTrue(browser.TextField(Find.ByName("issue.summary")).Exists, "Unable to find the summary text box. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.TextField(Find.ByName("issue.description")).Exists, "Unable to find the description text box. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.SelectList(Find.ByName("issue.assignedTo.id")).Exists, "Unable to find the assigned to select list. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.SelectList(Find.ByName("issue.reportedBy.id")).Exists, "Unable to find the reported by select list. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.Button(Find.ById("save")).Exists, "Unable to find the save button. The html is: {0}", browser.Html);
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
                browser.SelectList(Find.ByName("issue.assignedTo.id")).SelectByValue("0");
                browser.SelectList(Find.ByName("issue.reportedBy.id")).SelectByValue("0");

                browser.Button(Find.ById("save")).Click();

                Assert.IsTrue(browser.ContainsText("Created"), "Unable to find the created date column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Summary"), "Unable to find the summary column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Assigned"), "Unable to find the assigned to column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Reported"), "Unable to find the reported by column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText(summary), "Unable to find the summary text confirmation. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Unassigned"), "Unable to find the Unassigned text. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Anonymous"), "Unable to find the Anonymous text. The html is: {0}", browser.Html);
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
                Option assignedTo = browser.SelectList(Find.ByName("issue.assignedTo.id")).Option(Find.ByIndex(1));
                Option reportedBy = browser.SelectList(Find.ByName("issue.reportedBy.id")).Option(Find.ByIndex(1));

                Assert.AreEqual(1, assignedTo.Index, "There are no users in this database (can't select the assigned to user");
                Assert.AreEqual(1, reportedBy.Index, "There are no users in this database (can't select the reported by user");

                string assignedToText = assignedTo.Text;
                string reportedByText = reportedBy.Text;

                assignedTo.Select();
                reportedBy.Select();

                browser.Button(Find.ById("save")).Click();

                Assert.IsTrue(browser.ContainsText("Created"), "Unable to find the created date column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Summary"), "Unable to find the summary column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Assigned"), "Unable to find the assigned to column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText("Reported"), "Unable to find the reported by column header. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText(summary), "Unable to find the summary text confirmation. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText(assignedToText), "Unable to find the assigned to user name. The html is: {0}", browser.Html);
                Assert.IsTrue(browser.ContainsText(reportedByText), "Unable to find the reported by user name. The html is: {0}", browser.Html);
            }
        }
    }
}
