using System.Collections.Generic;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrabsWave.Test.TestHelpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerElementTextTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        private readonly ITestOutputHelper testOutput;

        public CrawlerElementTextTest(ITestOutputHelper output) => testOutput = output;

        public static IEnumerable<object[]> GetElementsToTestText() => new List<object[]> {
            new object[] { "btnOne", "This is a button", ElementsType.Id, true },
            new object[] { "btnOne", "This is a button", ElementsType.Name, true },
            new object[] { "P", "click using parameter", ElementsType.TagName, true },
            new object[] { "someClass", "This is a link with class", ElementsType.ClassName, true },
            new object[] { "body > a", "This is a link with class", ElementsType.CssSelector, true },
            new object[] { "click to increment", "click to increment", ElementsType.LinkText, true },
            new object[] { "click to increment", "click to increment", ElementsType.PartialLinkText, true },
            new object[] { "/html/body/form/a[1]", "click to increment", ElementsType.XPath, true },
            new object[] { "btnOne", "This is a button", ElementsType.Id, false }
        };

        public static IEnumerable<object[]> GetItemsForTestMultipleElements() => new List<object[]> {
            new object[] { "//*[@class='labels']" , ElementsType.XPath, "This is a label 1", false, true},
            new object[] { "aWrongXPath -123r" , ElementsType.XPath, "This is a label 1", true, true},
            new object[] { "aWrongXPath -123r" , ElementsType.XPath, "This is a label 1", true, false}
        };

        public static IEnumerable<object[]> GetItemsToClearAndSendKeys() => new List<object[]> {
            new object[] { "inputName", ElementsType.Id, "This is a test for Send using ID", false, false, "" },
            new object[] { "someWrongInputName", ElementsType.Id, "this is a test", false, true, "Could not get the element using identify:" },
            new object[] { "someWrongInputName", ElementsType.Id, "this is a test", true, true, "Could not get the element using identify:" }
        };

        [Theory]
        [MemberData(nameof(GetElementsToTestText))]
        public void ShouldGetElementText(string identify, string expectedValue, ElementsType elementsType, bool shouldRetry)
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logMoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .ElementInnerText(identify, elementsType, shouldRetry,  out var text);

                text.Should().Be(expectedValue);
            }
        }

        [Theory]
        [MemberData(nameof(GetItemsForTestMultipleElements))]
        public void ShouldGetTextFromMultipleElementsOcurrences(string identify, ElementsType elementsType, string textSample, bool shouldFail, bool shouldRetry)
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logMoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl($"file:///{PageForUnitTestHelper.GetPageForUnitTestWithMultipleItems()}", out _)
                   .ElementsText(identify, elementsType, shouldRetry, out var listOfText);

                if (!shouldFail)
                {
                    listOfText.Should().NotBeNull();
                    listOfText.Should().HaveCountGreaterThan(0);
                    listOfText.Should().ContainEquivalentOf(textSample);
                }
                else
                    listOfText.Should().BeNull();
            }
        }

        [Theory]
        [MemberData(nameof(GetItemsToClearAndSendKeys))]
        public void ShouldClearAndSendText(string identify, ElementsType elementsType, string textToSend, bool shouldRetry, bool shouldFail, string messageOnFail)
        {
            var (logMoq, logOutPut) = TestLoggerBuilder.Create<Crawler>();
            using (var sut = new Crawler(logMoq))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .ClearAndSendKeys(identify, elementsType, textToSend, shouldRetry)
                   .GetElementAttribute(identify, elementsType, "value", shouldRetry, out var text);

                testOutput.WriteLine(logOutPut.Output);

                if (!shouldFail)
                    text.Should().Be(textToSend);
                else
                    logOutPut.Output.Contains(messageOnFail).Should().BeTrue();
            }
        }
    }
}
