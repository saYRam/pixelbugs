using MbUnit.Framework;
using WatiN.Core;
using PixelDragons.Commons.TestSupport;

namespace PixelDragons.PixelBugs.Tests.Acceptance
{
    public class SecurityFixture : AcceptanceTestBase
    {
        [Test]
        public void AccessDenied_RequestActionWithoutCorrectPermission()
        {
            //Sign in as a user without the CreateCards permission, then request the Card/New page and check that
            //we are redirected to the access denied page.
            using (IE browser = new IE(BuildUrl("Security", "Index")))
            {
                browser.TextField(Find.ByName("userName")).TypeText("test.viewonly");
                browser.TextField(Find.ByName("password")).TypeText("password");

                browser.Button(Find.ById("signIn")).Click();

                Assert.IsFalse(browser.ContainsText("New Card"), "The new card link was not hidden. The html is: {0}", browser.Html);

                browser.GoTo(BuildUrl("Card", "New")); 
                
                Assert.IsTrue(browser.ContainsText("Access Denied"), "Access Denied title is not present. The html is: {0}", browser.Html);
            }
        }

        [Test]
        public void SignIn_EnterCorrectCredentials()
        {
            //Go to the sign in page a sign in with correct username and password. Confirm that we are on the card board page.
            using (IE browser = SignIn())
            {
                Assert.IsTrue(browser.ContainsText("Card Wall"), "Sign in failed as card board title is not present. The html is: {0}", browser.Html);
            }
        }

        [Test]
        public void SignIn_EnterInvalidCredentials()
        {
            //Go to the sign in page and sign in with wrong username and password. Confirm that we are redirected back to sign in page with message.
            using (IE browser = new IE(BuildUrl("Security", "Index")))
            {
                browser.TextField(Find.ByName("userName")).TypeText("invalid");
                browser.TextField(Find.ByName("password")).TypeText("credentials");

                browser.Button(Find.ById("signIn")).Click();

                Assert.IsTrue(browser.ContainsText("You entered an invalid user name or password"), "Invalid credentials error message is not present. The html is: {0}", browser.Html);
            }
        }

        [Test]
        public void SignOut_TryToAccessProtectedPage()
        {
            using (IE browser = new IE(BuildUrl("Security", "SignOut")))
            {
                //Navigating to the sign out page should clear out token cookie, so try to access a protected page
                browser.GoTo(BuildUrl("Card", "Index"));

                Assert.IsTrue(browser.ContainsText("Access Denied"), "Access Denied title is not present. The html is: {0}", browser.Html);
            }
        }

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
