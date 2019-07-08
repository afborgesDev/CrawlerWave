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
            new object[] { "btnOne", "This is a button", ElementsType.Id },
            new object[] { "btnOne", "This is a button", ElementsType.Name },
            new object[] { "P", "click using parameter", ElementsType.TagName },
            new object[] { "someClass", "This is a link with class", ElementsType.ClassName },
            new object[] { "body > a", "This is a link with class", ElementsType.CssSelector },
            new object[] { "click to increment", "click to increment", ElementsType.LinkText },
            new object[] { "click to increment", "click to increment", ElementsType.PartialLinkText },
            new object[] { "/html/body/form/a[1]", "click to increment", ElementsType.XPath }
        };

        public static IEnumerable<object[]> GetItemsForTestMultipleElements() => new List<object[]> {
            new object[] { "//*[@class='labels']" , ElementsType.XPath, "This is a label 1", false},
            new object[] { "aWrongXPath -123r" , ElementsType.XPath, "This is a label 1", true}
        };

        public static IEnumerable<object[]> GetItemsToClearAndSendKeys() => new List<object[]> {
            new object[] { "inputName", ElementsType.Id, "This is a test for Send using ID", false, "" },
            new object[] { "someWrongInputName", ElementsType.Id, "this is a test", true, "Cuold not clear and send key for element someWrongInputName" }
        };

        [Theory]
        [MemberData(nameof(GetElementsToTestText))]
        public void ShouldGetElementText(string identify, string expectedValue, ElementsType elementsType)
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logMoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .ElementInnerText(identify, elementsType, out var text);

                text.Should().Be(expectedValue);
            }
        }

        [Theory]
        [MemberData(nameof(GetItemsForTestMultipleElements))]
        public void ShouldGetTextFromMultipleElementsOcurrences(string identify, ElementsType elementsType, string textSample, bool shouldFail)
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logMoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl($"file:///{PageForUnitTestHelper.GetPageForUnitTestWithMultipleItems()}", out _)
                   .ElementsText(identify, elementsType, out var listOfText);

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
        public void ShouldClearAndSendText(string identify, ElementsType elementsType, string textToSend, bool shouldFail, string messageOnFail)
        {
            var (logMoq, logOutPut) = TestLoggerBuilder.Create<Crawler>();
            using (var sut = new Crawler(logMoq))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .ClearAndSendKeys(identify, elementsType, textToSend)
                   .GetElementAttribute(identify, elementsType, "value", out var text);

                testOutput.WriteLine(logOutPut.Output);
                //logOutPut.Output.Contains("Could not execute javascript and take a result").Should().BeTrue();

                if (!shouldFail)
                    text.Should().Be(textToSend);
                else
                    logOutPut.Output.Contains(messageOnFail).Should().BeTrue();
            }
        }
    }
}
