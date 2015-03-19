using System;
using System.Collections.Generic;
using BaseTypes = Gecko.BaseTypes;
using System.Text;
using NUnit.Framework;
using System.Windows.Forms;
using Gecko;
using System.IO;
using System.Runtime.InteropServices;
using Gecko.DOM;

namespace GeckofxUnitTests
{
    [TestFixture]
    internal class GeckoElementTests
    {
        private GeckoWebBrowser browser;

        [SetUp]
        public void BeforeEachTestSetup()
        {
            Xpcom.Initialize(XpComTests.XulRunnerLocation);
            browser = new GeckoWebBrowser();
            var unused = browser.Handle;
            Assert.IsNotNull(browser);
        }

        [TearDown]
        public void AfterEachTestTearDown()
        {
            browser.Dispose();
        }

        [Test]
        public void OuterHtml_AttributesHaveDoubleQuotes_()
        {
            string divString = "<div name=\"a\" id=\"_lv5\" class=\"none\">old value</div>";
            GeckoWebBrowserTextExtensionMethods.TestLoadHtml(browser, divString);
            //browser.TestLoadHtml(divString);

            var divElement = (browser.Document.Body.FirstChild as GeckoHtmlElement);
            Assert.AreEqual(divString.ToLowerInvariant(), divElement.OuterHtml.ToLowerInvariant());
        }

        [Test]
        public void OuterHtml_AttributesHaveSingleQuotes_()
        {
            string divString = "<div name=\'a\' id=\'_lv5\' class='none'>old value</div>";
            GeckoWebBrowserTextExtensionMethods.TestLoadHtml(browser, divString);
            //browser.TestLoadHtml(divString);

            var divElement = (browser.Document.Body.FirstChild as GeckoHtmlElement);
            Assert.AreEqual(divString.ToLowerInvariant().Replace('\'', '"'), divElement.OuterHtml.ToLowerInvariant());
        }

        [Test]
        public void SetId_SettingToEmptyString_IdAttributeIsRemoved()
        {
            GeckoWebBrowserTextExtensionMethods.TestLoadHtml(browser, "<div id=\"a\">hello</div>");
            //browser.TestLoadHtml("<div id=\"a\">hello</div>");

            var divElement = browser.Document.GetHtmlElementById("a");
            Assert.AreEqual("a", divElement.Id);

            divElement.Id = String.Empty;

            Assert.IsFalse(divElement.HasAttribute("id"));
        }

        [Test]
        public void SetId_SettingToNull_IdAttributeIsRemoved()
        {
            GeckoWebBrowserTextExtensionMethods.TestLoadHtml(browser, "<div id=\"a\">hello</div>");
            //browser.TestLoadHtml("<div id=\"a\">hello</div>");

            var divElement = browser.Document.GetHtmlElementById("a");
            Assert.AreEqual("a", divElement.Id);

            divElement.Id = null;

            Assert.IsFalse(divElement.HasAttribute("id"));
        }

        [Test]
        public void SetId_SettingToEmptyStringWhereIdIsMixedCase_IdAttributeIsRemoved()
        {
            GeckoWebBrowserTextExtensionMethods.TestLoadHtml(browser, "<div iD=\"a\">hello</div>");
            //browser.TestLoadHtml("<div iD=\"a\">hello</div>");

            var divElement = browser.Document.GetHtmlElementById("a");
            Assert.AreEqual("a", divElement.Id);

            divElement.Id = String.Empty;

            Assert.IsFalse(divElement.HasAttribute("iD"));
        }

        [Test]
        public void Style_GetInlineStyleWhenOneDoesNotExist_ValidGeckoStyleReturned()
        {
            GeckoWebBrowserTextExtensionMethods.TestLoadHtml(browser, "some random html");
            //browser.TestLoadHtml("some random html");

            var style = browser.Document.Body.Style;
            Assert.NotNull(style);

            // Test using it.
            style.SetPropertyValue("white-space", "pre-wrap");
        }
    }
}