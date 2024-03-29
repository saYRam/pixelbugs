﻿using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Tests.Integration.Base;
using WatiN.Core;

namespace PixelDragons.PixelBugs.Tests.Integration
{
    public class When_trying_to_access_the_application : IntegrationTestBase
    {
        [Test, Category("Integration")]
        public void Should_display_access_denied_view_when_accessing_page_without_correct_permissions()
        {
            //Sign in as a user without the CreateCards permission, then request the Card/New page and check that
            //we are redirected to the access denied page.
            using (IE browser = new IE(BuildUrl("Security", "Index")))
            {
                browser.TextField(Find.ByName("request.userName")).TypeText("test.viewonly");
                browser.TextField(Find.ByName("request.password")).TypeText("password");

                browser.Button(Find.ById("signIn")).Click();

                Assert.That(browser.ContainsText("New Card"), Is.False,
                            "The new card link was not hidden. The HTML is: {0}", browser.Html);

                browser.GoTo(BuildUrl("Card", "New"));

                Assert.That(browser.ContainsText("Access Denied"), Is.True,
                            "Access Denied title is not present. The HTML is: {0}", browser.Html);
            }
        }

        [Test, Category("Integration")]
        public void Should_sign_in_successfully_with_correct_credentials()
        {
            //Go to the sign in page a sign in with correct username and password. Confirm that we are on the card wall page.
            using (IE browser = SignIn())
            {
                Assert.That(browser.ContainsText("Card Wall"), Is.True,
                            "Sign in failed as card board title is not present. The HTML is: {0}", browser.Html);
            }
        }

        [Test, Category("Integration")]
        public void Should_not_allow_sign_in_with_incorrect_credentials()
        {
            //Go to the sign in page and sign in with wrong username and password. Confirm that we are redirected back to sign in page with message.
            using (IE browser = new IE(BuildUrl("Security", "Index")))
            {
                browser.TextField(Find.ByName("request.userName")).TypeText("invalid");
                browser.TextField(Find.ByName("request.password")).TypeText("credentials");

                browser.Button(Find.ById("signIn")).Click();

                Assert.That(browser.ContainsText("You entered an invalid user name or password"), Is.True,
                            "Invalid credentials error message is not present. The HTML is: {0}", browser.Html);
            }
        }

        [Test, Category("Integration")]
        public void Should_not_allow_sign_in_when_no_credentials_are_supplied()
        {
            //Go to the sign in page and sign in without entering a user name or password. Confirm that we are redirected back to sign in page with message.
            using (IE browser = new IE(BuildUrl("Security", "Index")))
            {
                browser.TextField(Find.ByName("request.userName")).TypeText("");
                browser.TextField(Find.ByName("request.password")).TypeText("");

                browser.Button(Find.ById("signIn")).Click();

                Assert.That(browser.ContainsText("You entered an invalid user name or password"), Is.True,
                            "Invalid credentials error message is not present. The HTML is: {0}", browser.Html);
            }
        }

        [Test, Category("Integration")]
        public void Should_not_allow_access_to_protected_pages_after_sign_out()
        {
            using (IE browser = new IE(BuildUrl("Security", "SignOut")))
            {
                //Navigating to the sign out page should clear out token cookie, so try to access a protected page
                browser.GoTo(BuildUrl("Card", "Index"));

                Assert.That(browser.ContainsText("Access Denied"), Is.True,
                            "Access Denied title is not present. The HTML is: {0}", browser.Html);
            }
        }
    }
}