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
            new object[] { WebElementType.Id("btnOne"), "This is a button", true },
            new object[] { WebElementType.Name("btnOne"), "This is a button", true },
            new object[] { WebElementType.TagName("P"), "click using parameter", true },
            new object[] { WebElementType.ClassName("someClass"), "This is a link with class", true },
            new object[] { WebElementType.CssSelector("body > a"), "This is a link with class", true },
            new object[] { WebElementType.LinkText("click to increment"), "click to increment", true },
            new object[] { WebElementType.PartialLinkText("click to increment"), "click to increment", true },
            new object[] { WebElementType.XPath("/html/body/form/a[1]"), "click to increment", true },
            new object[] { WebElementType.Id("btnOne"), "This is a button", false }
        };

        public static IEnumerable<object[]> GetItemsForTestMultipleElements() => new List<object[]> {
            new object[] { WebElementType.XPath("//*[@class='labels']"), "This is a label 1", false, true},
            new object[] { WebElementType.XPath("aWrongXPath -123r"), "This is a label 1", true, true},
            new object[] { WebElementType.XPath("aWrongXPath -123r"), "This is a label 1", true, false}
        };

        public static IEnumerable<object[]> GetItemsToClearAndSendKeys() => new List<object[]> {
            new object[] { WebElementType.Id("inputName"), "This is a test for Send using ID", false, false, "" },
            new object[] { WebElementType.Id("someWrongInputName"), "this is a test", false, true, "Could not get the element using identify:" },
            new object[] { WebElementType.Id("someWrongInputName"), "this is a test", true, true, "Could not get the element using identify:" }
        };

        [Theory]
        [MemberData(nameof(GetElementsToTestText))]
        public void ShouldGetElementText(WebElementType webElementType, string expectedValue, bool shouldRetry)
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .ElementInnerText(webElementType, shouldRetry, out var text);

                text.Should().Be(expectedValue);
            }
        }

        [Theory]
        [MemberData(nameof(GetItemsForTestMultipleElements))]
        public void ShouldGetTextFromMultipleElementsOcurrences(WebElementType webElementType, string textSample, bool shouldFail, bool shouldRetry)
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl($"file:///{PageForUnitTestHelper.GetPageForUnitTestWithMultipleItems()}", out _)
                   .ElementsText(webElementType, shouldRetry, out var listOfText);

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
        public void ShouldClearAndSendText(WebElementType webElementType, string textToSend, bool shouldRetry, bool shouldFail, string messageOnFail)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .ClearAndSendKeys(webElementType, textToSend, shouldRetry)
                   .GetElementAttribute(webElementType, "value", shouldRetry, out var text);

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
