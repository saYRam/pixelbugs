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
            using (IE browser = new IE(BuildUrl("Issue", "New")))
            {
                //Check the text of the rendered html
                Assert.IsTrue(browser.ContainsText("New Issue"), 
                    String.Format("New issue page doesn't contain the correct page title. The html is: {0}", browser.Html));
                Assert.IsTrue(browser.ContainsText("Summary"),
                    String.Format("New issue page doesn't contain the summary label. The html is: {0}", browser.Html));
                Assert.IsTrue(browser.ContainsText("Description"),
                    String.Format("New issue page doesn't contain the description label. The html is: {0}", browser.Html));

                //Check the form controls are part of the rendered html
                Assert.IsNotNull(browser.TextField(Find.ByName("issue.summary")).HTMLElement, 
                    "Unable to find the summary text box. The html is: {0}", browser.Html);
                Assert.IsNotNull(browser.TextField(Find.ByName("issue.description")).HTMLElement, 
                    "Unable to find the description text box. The html is: {0}", browser.Html);
                Assert.IsNotNull(browser.Button(Find.ById("save")).HTMLElement, 
                    "Unable to find the save button. The html is: {0}", browser.Html);
            }
        }

        [Test]
        public void New_CompleteFormAndSubmit()
        {
            using (IE browser = new IE(BuildUrl("Issue", "New")))
            {
                Guid testId = Guid.NewGuid();
                string summary = String.Format("Summary {0}", testId);
                string description = String.Format("Description {0}", testId);

                browser.TextField(Find.ByName("issue.summary")).TypeText(summary);
                browser.TextField(Find.ByName("issue.description")).TypeText(description);

                browser.Button(Find.ById("save")).Click();

                Assert.IsTrue(browser.ContainsText(summary),
                    "Unable to find the summary text confirmation. The html is: {0}", browser.Html);
            }
        }
    }
}
