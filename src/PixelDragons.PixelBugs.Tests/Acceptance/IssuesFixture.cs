using System;
using MbUnit.Framework;
using System.Threading;
using WatiN.Core;

namespace PixelDragons.PixelBugs.Tests.Acceptance
{
    [TestFixture(ApartmentState = ApartmentState.STA)]
    public class IssuesFixture
    {
        [Test]
        public void New_RenderNewIssuePage()
        {
            using (IE browser = new IE("http://localhost:1323/Issue/New.ashx"))
            {
                //Check the text of the rendered html
                Assert.IsTrue(browser.ContainsText("New Issue"), 
                    String.Format("New issue page doesn't contain the correct page title. The html is: {0}", browser.Html));
                Assert.IsTrue(browser.ContainsText("Summary"),
                    String.Format("New issue page doesn't contain the summary label. The html is: {0}", browser.Html));
                Assert.IsTrue(browser.ContainsText("Description"),
                    String.Format("New issue page doesn't contain the description label. The html is: {0}", browser.Html));

                //Check the form controls are part of the rendered html
                Assert.IsNotNull(browser.TextField(Find.ByName("issue.summary")).HTMLElement);
                Assert.IsNotNull(browser.TextField(Find.ByName("issue.description")).HTMLElement);
                Assert.IsNotNull(browser.Button(Find.ByValue("Save")).HTMLElement);
            }
        }
    }
}
