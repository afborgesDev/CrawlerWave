using System.Collections.Generic;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerClickTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClicElement(string url, string identify, ElementsType type, bool shouldFail)
        {
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .Click(identify, type);

                var timesToCheck = Times.Never();
                if (shouldFail) timesToCheck = Times.AtLeastOnce();

                logmoq.VerifyNearLog(LogLevel.Error, "Could not", timesToCheck);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClickUsingScript(string url, string identify, ElementsType type, bool shouldFail)
        {
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .ClickUsingScript(identify, type);

                var timesToCheck = Times.Never();
                if (shouldFail) timesToCheck = Times.AtLeastOnce();

                logmoq.VerifyNearLog(LogLevel.Error, "Could not", timesToCheck);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClick))]
        public void ShouldClickFirst(string url, string identify, ElementsType type, bool shouldFail)
        {
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .ClickFirst(identify, type);

                var timesToCheck = Times.Never();
                if (shouldFail) timesToCheck = Times.AtLeastOnce();

                logmoq.VerifyNearLog(LogLevel.Error, "Could not", timesToCheck);
            }
        }

        [Theory]
        [MemberData(nameof(GetElementsToClickWithCondition))]
        public void ShouldClickIfTrue(string url, string identify, ElementsType elements, string idNumberResult, bool condition)
        {
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(url, out _)
                   .GetElement(idNumberResult, ElementsType.Id,out var checkElement);

                int.TryParse(checkElement.GetAttribute("value"), out var elementValueBefore);

                sut.ClickIfTrue(identify, condition, elements)
                   .GetElement(idNumberResult, ElementsType.Id, out checkElement);

                int.TryParse(checkElement.GetAttribute("value"), out var elementValueAfter);

                if (condition)
                    elementValueAfter.Should().BeGreaterThan(elementValueBefore);
                else
                    elementValueAfter.Should().Be(elementValueBefore);
            }
        }

        [Fact]
        public void ShoulConfirmAlert()
        {
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .Click("clickToAlert", ElementsType.Id)
                   .ClickAlert(true)
                   .GetElement("numberResult", ElementsType.Id, out var elementResult);
                int.TryParse(elementResult.GetAttribute("value"), out var value);

                value.Should().Be(23);
            }
        }

        [Fact]
        public void ShoulDemissAlert()
        {
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .Click("clickToAlert", ElementsType.Id)
                   .ClickAlert(false)
                   .GetElement("numberResult", ElementsType.Id, out var elementResult);
                int.TryParse(elementResult.GetAttribute("value"), out var value);

                value.Should().Be(-1);
            }
        }

        public static IEnumerable<object[]> GetElementsToClick() => new List<object[]> {
                new object[] { LocalUrl, "//*[@id='ButtonsToXPath']", ElementsType.XPath, false },
                new object[] { LocalUrl, "inputName", ElementsType.Name, false },
                new object[] { LocalUrl, "INPUT", ElementsType.TagName, false},
                new object[] { LocalUrl, "body > a", ElementsType.CssSelector, false},
                new object[] { LocalUrl, "btnOne", ElementsType.Id, false},
                new object[] { LocalUrl, "someClass", ElementsType.ClassName, false },
                new object[] { LocalUrl, "click to increment", ElementsType.PartialLinkText, false},
                new object[] { LocalUrl, "click to increment" , ElementsType.LinkText, false}
        };

        public static IEnumerable<object[]> GetElementsToClickWithCondition() => new List<object[]> {
            new object[] { LocalUrl,  "buttonIncrement", ElementsType.Id, "numberResult", true},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.Name, "numberResult", true},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.ClassName, "numberResult", true},
            new object[] { LocalUrl, "//*[@id='buttonIncrement']", ElementsType.XPath, "numberResult", true},
            new object[] { LocalUrl, "#buttonIncrement", ElementsType.CssSelector, "numberResult", true},
            new object[] { LocalUrl, "click to increment", ElementsType.PartialLinkText, "numberResult", true},
            new object[] { LocalUrl, "click to increment" , ElementsType.LinkText, "numberResult", true},
            new object[] { LocalUrl, "P" , ElementsType.TagName, "numberResult", true},
            new object[] { LocalUrl,  "buttonIncrement", ElementsType.Id, "numberResult", false},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.Name, "numberResult", false},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.ClassName, "numberResult", false},
            new object[] { LocalUrl, "//*[@id='buttonIncrement']", ElementsType.XPath, "numberResult", false},
            new object[] { LocalUrl, "#buttonIncrement", ElementsType.CssSelector, "numberResult", false},
            new object[] { LocalUrl, "click to increment", ElementsType.PartialLinkText, "numberResult", false},
            new object[] { LocalUrl, "click to increment" , ElementsType.LinkText, "numberResult", false},
            new object[] { LocalUrl, "P" , ElementsType.TagName, "numberResult", false}
        };
    }
}
