using System.Collections.Generic;
using System.Linq;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrawlerWave.LogTestUtils;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerElementTextTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        public static IEnumerable<object[]> GetElementsToTestText() => new List<object[]> {
            new object[] { WebElementType.Id("btnOne", true), "This is a button" },
            new object[] { WebElementType.Name("btnOne", true), "This is a button"},
            new object[] { WebElementType.TagName("P", true), "click using parameter"},
            new object[] { WebElementType.ClassName("someClass", true), "This is a link with class"},
            new object[] { WebElementType.CssSelector("body > a", true), "This is a link with class"},
            new object[] { WebElementType.LinkText("click to increment", true), "click to increment" },
            new object[] { WebElementType.PartialLinkText("click to increment", true), "click to increment" },
            new object[] { WebElementType.XPath("/html/body/form/a[1]", true), "click to increment" },
            new object[] { WebElementType.Id("btnOne", false), "This is a button" }
        };

        public static IEnumerable<object[]> GetItemsForTestMultipleElements() => new List<object[]> {
            new object[] { WebElementType.XPath("//*[@class='labels']", true), "This is a label 1", false},
            new object[] { WebElementType.XPath("aWrongXPath -123r", true), "This is a label 1", true},
            new object[] { WebElementType.XPath("aWrongXPath -123r", false), "This is a label 1", true}
        };

        public static IEnumerable<object[]> GetItemsToClearAndSendKeys() => new List<object[]> {
            new object[] { WebElementType.Id("inputName", false), "This is a test for Send using ID", false, "" },
            new object[] { WebElementType.Id("someWrongInputName", false), "this is a test", true, "Could not get the element using identify:" },
            new object[] { WebElementType.Id("someWrongInputName", true), "this is a test", true, "Could not get the element using identify:" }
        };

        [Theory]
        [MemberData(nameof(GetElementsToTestText))]
        public void ShouldGetElementText(WebElementType webElementType, string expectedValue)
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .ElementInnerText(webElementType, out var text);

                text.Should().Be(expectedValue);
            }
        }

        [Theory]
        [MemberData(nameof(GetItemsForTestMultipleElements))]
        public void ShouldGetTextFromMultipleElementsOcurrences(WebElementType webElementType, string textSample, bool shouldFail)
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl($"file:///{PageForUnitTestHelper.GetPageForUnitTestWithMultipleItems()}", out _)
                   .ElementsText(webElementType, out var listOfText);

                if (!shouldFail)
                {
                    listOfText.Should().NotBeNull();
                    listOfText.Should().HaveCountGreaterThan(0);
                    listOfText.Should().ContainEquivalentOf(textSample);
                }
                else
                {
                    listOfText.Should().BeNull();
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetItemsToClearAndSendKeys))]
        public void ShouldClearAndSendText(WebElementType webElementType, string textToSend, bool shouldFail, string messageOnFail)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .ClearAndSendKeys(webElementType, textToSend)
                   .GetElementAttribute(webElementType, "value", out var text);

                if (!shouldFail)
                {
                    text.Should().Be(textToSend);
                }
                else
                {
                    testSink.Writes.Any(x => x.Message.Contains(messageOnFail,
                                           System.StringComparison.InvariantCultureIgnoreCase)).Should().BeTrue();
                }
            }
        }
    }
}
