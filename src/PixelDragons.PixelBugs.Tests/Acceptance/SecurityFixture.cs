﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using WatiN.Core;

namespace PixelDragons.PixelBugs.Tests.Acceptance
{
    public class SecurityFixture : AcceptanceTestBase
    {
        [Test]
        public void Timeout_UserShouldBeDeniedAccessAndRedirected()
        { 
            //Request a secure page without logging in and confirm that we are redirected to a timeout page.
            using (IE browser = new IE(BuildUrl("Issue", "List")))
            {
                Assert.IsTrue(browser.ContainsText("Session Timeout"), "The timeout page doesn't contain the correct title. The html is: {0}", browser.Html);
            }
        }

        [Test]
        public void AccessDenied_RequestActionWithoutCorrectPermission()
        {
            //Sign in as a user without the CreateIssues permission, then request the Issue/New page and check that
            //we are redirected to the access denied page.
            using (IE browser = new IE(BuildUrl("Security", "Index")))
            {
                browser.TextField(Find.ByName("userName")).TypeText("test.viewonly");
                browser.TextField(Find.ByName("password")).TypeText("password");

                browser.Button(Find.ById("signIn")).Click();

                Assert.IsFalse(browser.ContainsText("New Issue"), "The new issue link was not hidden. The html is: {0}", browser.Html);

                browser.GoTo(BuildUrl("Issue", "New")); 
                
                Assert.IsTrue(browser.ContainsText("Access Denied"), "Access Denied title is not present. The html is: {0}", browser.Html);
            }
        }

        [Test]
        public void SignIn_EnterCorrectCredentials()
        {
            //Go to the sign in page a sign in with correct username and password. Confirm that we are on the issues list page.
            using (IE browser = SignIn())
            {
                Assert.IsTrue(browser.ContainsText("Issues List"), "Sign in failed as issues list title is not present. The html is: {0}", browser.Html);
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

        public IE SignIn()
        {
            IE browser = new IE(BuildUrl("Security", "Index"));

            browser.TextField(Find.ByName("userName")).TypeText("test.admin");
            browser.TextField(Find.ByName("password")).TypeText("password");

            browser.Button(Find.ById("signIn")).Click();

            return browser;
        }
    }
}
