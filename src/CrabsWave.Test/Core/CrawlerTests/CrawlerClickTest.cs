using System.Collections.Generic;
using System.Linq;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrawlerWave.LogTestUtils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerClickTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        public static IEnumerable<object[]> GetElementsToClick() => new List<object[]> {
                new object[] { LocalUrl, WebElementType.XPath("//*[@id='ButtonsToXPath']"), false, true },
                new object[] { LocalUrl, WebElementType.Name("inputName"), false, true },
                new object[] { LocalUrl, WebElementType.TagName("INPUT"), false, true },
                new object[] { LocalUrl, WebElementType.CssSelector("body > a"), false, true },
                new object[] { LocalUrl, WebElementType.Id("btnOne"), false, true },
                new object[] { LocalUrl, WebElementType.ClassName("someClass"), false, true },
                new object[] { LocalUrl, WebElementType.PartialLinkText("click to increment"), false, true },
                new object[] { LocalUrl, WebElementType.LinkText("click to increment" ), false, true }
        };

        public static IEnumerable<object[]> GetElementsToClickWithCondition() => new List<object[]> {
            new object[] { LocalUrl, WebElementType.Id("buttonIncrement"), "numberResult", true, true},
            new object[] { LocalUrl, WebElementType.Name("buttonIncrement"), "numberResult", true, true},
            new object[] { LocalUrl, WebElementType.ClassName("buttonIncrement"), "numberResult", true, true},
            new object[] { LocalUrl, WebElementType.XPath("//*[@id='buttonIncrement']"), "numberResult", true, true},
            new object[] { LocalUrl, WebElementType.CssSelector("#buttonIncrement"), "numberResult", true, true},
            new object[] { LocalUrl, WebElementType.PartialLinkText("click to increment"), "numberResult", true, true},
            new object[] { LocalUrl, WebElementType.LinkText("click to increment" ), "numberResult", true, true},
            new object[] { LocalUrl, WebElementType.TagName("P" ), "numberResult", true, true},
            new object[] { LocalUrl, WebElementType.Id("buttonIncrement"), "numberResult", false, true},
            new object[] { LocalUrl, WebElementType.Name("buttonIncrement"), "numberResult", false, true},
            new object[] { LocalUrl, WebElementType.ClassName("buttonIncrement"), "numberResult", false, true},
            new object[] { LocalUrl, WebElementType.XPath("//*[@id='buttonIncrement']"), "numberResult", false, true},
            new object[] { LocalUrl, WebElementType.CssSelector("#buttonIncrement"), "numberResult", false, true},
            new object[] { LocalUrl, WebElementType.PartialLinkText("click to increment"), "numberResult", false, true},
            new object[] { LocalUrl, WebElementType.LinkText("click to increment" ), "numberResult", false, true},
            new object[] { LocalUrl, WebElementType.TagName("P" ), "numberResult", false, true },
            new object[] { LocalUrl, WebElementType.TagName("P" ), "numberResult", false, false }
        };

        public static IEnumerable<object[]> GetElementsToFailOnClick() => new List<object[]> {
            new object[] { LocalUrl, WebElementType.Id("sometingWrong"), false },
        };

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClicElement(string url, WebElementType webElementType, bool shouldFail, bool shouldRetry)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .Click(webElementType, shouldRetry);

                var logInformation = testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"));
                logInformation.Should().Be(shouldFail);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClickUsingScript(string url, WebElementType webElementType, bool shouldFail, bool shouldRetry)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .ClickUsingScript(webElementType, shouldRetry);

                var logInformation = testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"));
                logInformation.Should().Be(shouldFail);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClickFirst(string url, WebElementType webElementType, bool shouldFail, bool shouldRetry)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .ClickFirst(webElementType, shouldRetry);

                var logInformation = testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"));
                logInformation.Should().Be(shouldFail);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClickWithCondition))]
        public void ShouldClickIfTrue(string url, WebElementType webElementType, string idNumberResult, bool condition, bool shouldRetry)
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .GetElement(webElementType, shouldRetry, out var checkElement);

                int.TryParse(checkElement.GetAttribute("value"), out var elementValueBefore);

                sut.ClickIfTrue(webElementType, condition, shouldRetry)
                   .GetElement(WebElementType.Id(idNumberResult), shouldRetry, out checkElement);

                int.TryParse(checkElement.GetAttribute("value"), out var elementValueAfter);

                if (condition)
                    elementValueAfter.Should().BeGreaterThan(elementValueBefore);
                else
                    elementValueAfter.Should().Be(elementValueBefore);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToFailOnClick))]
        public void ShouldNotClickBecouseCoundFind(string url, WebElementType webElementType, bool shouldRetry)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .Click(webElementType, shouldRetry)
                   .GetElementAttribute(WebElementType.Id("numberResult"), "value", shouldRetry, out var elementResult);

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
                   .Click(WebElementType.Id("clickToAlert"), false)
                   .ClickAlert(true)
                   .GetElement(WebElementType.Id("numberResult"), true, out var elementResult);
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
                   .Click(WebElementType.Id("clickToAlert"), false)
                   .ClickAlert(false)
                   .GetElement(WebElementType.Id("numberResult"), true, out var elementResult);
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
                   .ClickFirst(WebElementType.Id("buttonIncrement-123"), false)
                   .GetElementAttribute(WebElementType.Id("numberResult"), "value", false, out var elementResult);

                int.TryParse(elementResult, out var value);

                value.Should().Be(0);
            }
        }
    }
}
