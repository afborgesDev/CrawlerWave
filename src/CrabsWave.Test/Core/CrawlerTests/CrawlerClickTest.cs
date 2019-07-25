using System.Collections.Generic;
using System.Linq;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrawlerWave.LogTestHelper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerClickTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        public static IEnumerable<object[]> GetElementsToClick() => new List<object[]> {
                new object[] { LocalUrl, WebElementType.XPath("//*[@id='ButtonsToXPath']", true), false },
                new object[] { LocalUrl, WebElementType.Name("inputName", true), false },
                new object[] { LocalUrl, WebElementType.TagName("INPUT", true), false },
                new object[] { LocalUrl, WebElementType.CssSelector("body > a", true), false },
                new object[] { LocalUrl, WebElementType.Id("btnOne", true), false},
                new object[] { LocalUrl, WebElementType.ClassName("someClass", true), false },
                new object[] { LocalUrl, WebElementType.PartialLinkText("click to increment", true), false },
                new object[] { LocalUrl, WebElementType.LinkText("click to increment", true), false }
        };

        public static IEnumerable<object[]> GetElementsToClickWithCondition() => new List<object[]> {
            new object[] { LocalUrl, WebElementType.Id("buttonIncrement", true), "numberResult", true},
            new object[] { LocalUrl, WebElementType.Name("buttonIncrement", true), "numberResult", true},
            new object[] { LocalUrl, WebElementType.ClassName("buttonIncrement", true), "numberResult", true},
            new object[] { LocalUrl, WebElementType.XPath("//*[@id='buttonIncrement']", true), "numberResult", true},
            new object[] { LocalUrl, WebElementType.CssSelector("#buttonIncrement", true), "numberResult", true},
            new object[] { LocalUrl, WebElementType.PartialLinkText("click to increment", true), "numberResult", true},
            new object[] { LocalUrl, WebElementType.LinkText("click to increment", true), "numberResult", true},
            new object[] { LocalUrl, WebElementType.TagName("P", true), "numberResult", true},
            new object[] { LocalUrl, WebElementType.Id("buttonIncrement", true), "numberResult", false},
            new object[] { LocalUrl, WebElementType.Name("buttonIncrement", true), "numberResult", false},
            new object[] { LocalUrl, WebElementType.ClassName("buttonIncrement", true), "numberResult", false},
            new object[] { LocalUrl, WebElementType.XPath("//*[@id='buttonIncrement']", true), "numberResult", false},
            new object[] { LocalUrl, WebElementType.CssSelector("#buttonIncrement", true), "numberResult", false},
            new object[] { LocalUrl, WebElementType.PartialLinkText("click to increment", true), "numberResult", false},
            new object[] { LocalUrl, WebElementType.LinkText("click to increment", true), "numberResult", false},
            new object[] { LocalUrl, WebElementType.TagName("P", true), "numberResult", false},
            new object[] { LocalUrl, WebElementType.TagName("P", false), "numberResult", false }
        };

        public static IEnumerable<object[]> GetElementsToFailOnClick() => new List<object[]> {
            new object[] { LocalUrl, WebElementType.Id("sometingWrong", false) },
        };

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClicElement(string url, WebElementType webElementType, bool shouldFail)
        {
            var (testSink, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .Click(webElementType);

                var logInformation = testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"));
                logInformation.Should().Be(shouldFail);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClickUsingScript(string url, WebElementType webElementType, bool shouldFail)
        {
            var (testSink, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .ClickUsingScript(webElementType);

                var logInformation = testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"));
                logInformation.Should().Be(shouldFail);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClickFirst(string url, WebElementType webElementType, bool shouldFail)
        {
            var (testSink, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .ClickFirst(webElementType);

                var logInformation = testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"));
                logInformation.Should().Be(shouldFail);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClickWithCondition))]
        public void ShouldClickIfTrue(string url, WebElementType webElementType, string idNumberResult, bool condition)
        {
            var (_, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .GetElement(webElementType, out var checkElement);

                int.TryParse(checkElement.GetAttribute("value"), out var elementValueBefore);

                sut.ClickIfTrue(webElementType, condition)
                   .GetElement(WebElementType.Id(idNumberResult), out checkElement);

                int.TryParse(checkElement.GetAttribute("value"), out var elementValueAfter);

                if (condition)
                    elementValueAfter.Should().BeGreaterThan(elementValueBefore);
                else
                    elementValueAfter.Should().Be(elementValueBefore);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToFailOnClick))]
        public void ShouldNotClickBecouseCoundFind(string url, WebElementType webElementType)
        {
            var (testSink, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .Click(webElementType)
                   .GetElementAttribute(WebElementType.Id("numberResult"), "value", out var elementResult);

                int.TryParse(elementResult, out var value);
                value.Should().Be(0);
                testSink.Writes.Any(x => x.LogLevel == LogLevel.Error && x.Message.Contains("Could not"))
                        .Should().BeTrue();
            }
        }

        [Fact]
        public void ShoulConfirmAlert()
        {
            var (_, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .Click(WebElementType.Id("clickToAlert"))
                   .ClickAlert(true)
                   .GetElement(WebElementType.Id("numberResult", true), out var elementResult);
                int.TryParse(elementResult.GetAttribute("value"), out var value);

                value.Should().Be(23);
            }
        }

        [Fact]
        public void ShoulDemissAlert()
        {
            var (_, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .Click(WebElementType.Id("clickToAlert"))
                   .ClickAlert(false)
                   .GetElement(WebElementType.Id("numberResult", true), out var elementResult);
                int.TryParse(elementResult.GetAttribute("value"), out var value);

                value.Should().Be(-1);
            }
        }

        [Fact]
        public void ShouldTestClickFirstManager()
        {
            var (_, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .ClickFirst(WebElementType.Id("buttonIncrement-123"))
                   .GetElementAttribute(WebElementType.Id("numberResult"), "value", out var elementResult);

                int.TryParse(elementResult, out var value);

                value.Should().Be(0);
            }
        }
    }
}
