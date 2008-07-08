using PixelDragons.Commons.TestSupport;
using WatiN.Core;

namespace PixelDragons.PixelBugs.Tests.Acceptance
{
    public class AcceptanceHelper : AcceptanceTestBase
    {
        public IE SignIn()
        {
            IE browser = new IE(BuildUrl("Security", "Index"));

            browser.TextField(Find.ByName("userName")).TypeText("test.admin");
            browser.TextField(Find.ByName("password")).TypeText("password");

            browser.Button(Find.ById("signIn")).Click();

            return browser;
        }

        public void SignOut(IE browser)
        {
            browser.GoTo(BuildUrl("Security", "SignOut"));
        }
    }
}