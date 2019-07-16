using System.Collections.Generic;
using System.Linq;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrawlerWave.LogTestUtils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerClickTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        public static IEnumerable<object[]> GetElementsToClick() => new List<object[]> {
                new object[] { LocalUrl, "//*[@id='ButtonsToXPath']", ElementsType.XPath, false, true },
                new object[] { LocalUrl, "inputName", ElementsType.Name, false, true },
                new object[] { LocalUrl, "INPUT", ElementsType.TagName, false, true },
                new object[] { LocalUrl, "body > a", ElementsType.CssSelector, false, true },
                new object[] { LocalUrl, "btnOne", ElementsType.Id, false, true },
                new object[] { LocalUrl, "someClass", ElementsType.ClassName, false, true },
                new object[] { LocalUrl, "click to increment", ElementsType.PartialLinkText, false, true },
                new object[] { LocalUrl, "click to increment" , ElementsType.LinkText, false, true }
        };

        public static IEnumerable<object[]> GetElementsToClickWithCondition() => new List<object[]> {
            new object[] { LocalUrl,  "buttonIncrement", ElementsType.Id, "numberResult", true, true},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.Name, "numberResult", true, true},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.ClassName, "numberResult", true, true},
            new object[] { LocalUrl, "//*[@id='buttonIncrement']", ElementsType.XPath, "numberResult", true, true},
            new object[] { LocalUrl, "#buttonIncrement", ElementsType.CssSelector, "numberResult", true, true},
            new object[] { LocalUrl, "click to increment", ElementsType.PartialLinkText, "numberResult", true, true},
            new object[] { LocalUrl, "click to increment" , ElementsType.LinkText, "numberResult", true, true},
            new object[] { LocalUrl, "P" , ElementsType.TagName, "numberResult", true, true},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.Id, "numberResult", false, true},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.Name, "numberResult", false, true},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.ClassName, "numberResult", false, true},
            new object[] { LocalUrl, "//*[@id='buttonIncrement']", ElementsType.XPath, "numberResult", false, true},
            new object[] { LocalUrl, "#buttonIncrement", ElementsType.CssSelector, "numberResult", false, true},
            new object[] { LocalUrl, "click to increment", ElementsType.PartialLinkText, "numberResult", false, true},
            new object[] { LocalUrl, "click to increment" , ElementsType.LinkText, "numberResult", false, true},
            new object[] { LocalUrl, "P" , ElementsType.TagName, "numberResult", false, true },
            new object[] { LocalUrl, "P" , ElementsType.TagName, "numberResult", false, false }
        };

        public static IEnumerable<object[]> GetElementsToFailOnClick() => new List<object[]> {
            new object[] { LocalUrl, "sometingWrong", ElementsType.Id, false },
        };

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClicElement(string url, string identify, ElementsType type, bool shouldFail, bool shouldRetry)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .Click(identify, type, shouldRetry);

                var logInformation = testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"));
                logInformation.Should().Be(shouldFail);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClickUsingScript(string url, string identify, ElementsType type, bool shouldFail, bool shouldRetry)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .ClickUsingScript(identify, type, shouldRetry);

                var logInformation = testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"));
                logInformation.Should().Be(shouldFail);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClickFirst(string url, string identify, ElementsType type, bool shouldFail, bool shouldRetry)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .ClickFirst(identify, type, shouldRetry);

                var logInformation = testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"));
                logInformation.Should().Be(shouldFail);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClickWithCondition))]
        public void ShouldClickIfTrue(string url, string identify, ElementsType elements, string idNumberResult, bool condition, bool shouldRetry)
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .GetElement(idNumberResult, ElementsType.Id, shouldRetry, out var checkElement);

                int.TryParse(checkElement.GetAttribute("value"), out var elementValueBefore);

                sut.ClickIfTrue(identify, condition, elements, shouldRetry)
                   .GetElement(idNumberResult, ElementsType.Id, shouldRetry, out checkElement);

                int.TryParse(checkElement.GetAttribute("value"), out var elementValueAfter);

                if (condition)
                    elementValueAfter.Should().BeGreaterThan(elementValueBefore);
                else
                    elementValueAfter.Should().Be(elementValueBefore);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToFailOnClick))]
        public void ShouldNotClickBecouseCoundFind(string url, string identify, ElementsType elementsType, bool shouldRetry)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .Click(identify, elementsType, shouldRetry)
                   .GetElementAttribute("numberResult", ElementsType.Id, "value", shouldRetry, out var elementResult);

                int.TryParse(elementResult, out var value);
                value.Should().Be(0);
                testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"))
                        .Should().BeTrue();
            }
        }

        [Fact]
        public void ShoulConfirmAlert()
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .Click("clickToAlert", ElementsType.Id, false)
                   .ClickAlert(true)
                   .GetElement("numberResult", ElementsType.Id, true, out var elementResult);
                int.TryParse(elementResult.GetAttribute("value"), out var value);

                value.Should().Be(23);
            }
        }

        [Fact]
        public void ShoulDemissAlert()
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .Click("clickToAlert", ElementsType.Id, false)
                   .ClickAlert(false)
                   .GetElement("numberResult", ElementsType.Id, true, out var elementResult);
                int.TryParse(elementResult.GetAttribute("value"), out var value);

                value.Should().Be(-1);
            }
        }

        [Fact]
        public void ShouldTestClickFirstManager()
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .ClickFirst("buttonIncrement-123", ElementsType.Id, false)
                   .GetElementAttribute("numberResult", ElementsType.Id, "value", false, out var elementResult);

                int.TryParse(elementResult, out var value);

                value.Should().Be(0);
            }
        }
    }
}
