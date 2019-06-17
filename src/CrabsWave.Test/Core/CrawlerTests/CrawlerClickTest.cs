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
                   .GoToUrl(url, out _);

                switch (type)
                {
                    case ElementsType.Id:
                        sut.ClickById(identify);
                        break;
                    case ElementsType.Name:
                        sut.ClickByName(identify);
                        break;
                    case ElementsType.TagName:
                        sut.ClickByTagName(identify);
                        break;
                    case ElementsType.ClassName:
                        sut.ClickByClassName(identify);
                        break;
                    case ElementsType.CssSelector:
                        sut.ClickByCssSelector(identify);
                        break;
                    case ElementsType.LinkText:
                        sut.ClickByLinkText(identify);
                        break;
                    case ElementsType.PartialLinkText:
                        sut.ClickByPartialLinkText(identify);
                        break;
                    default:
                        sut.ClickByXPath(identify);
                        break;
                }

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
                   .GoToUrl(url, out _);

                switch (type)
                {
                    case ElementsType.Id:
                        sut.ClickByIdUsingScript(identify);
                        break;
                    case ElementsType.Name:
                        sut.ClickByNameUsingScript(identify);
                        break;
                    case ElementsType.TagName:
                        sut.ClickByTagNameUsingScript(identify);
                        break;
                    case ElementsType.ClassName:
                        sut.ClickByClassNameUsingScript(identify);
                        break;
                    case ElementsType.CssSelector:
                        sut.ClickByCssSelectorUsingScript(identify);
                        break;
                    case ElementsType.LinkText:
                        sut.ClickByLinkTextUsingScript(identify);
                        break;
                    case ElementsType.PartialLinkText:
                        sut.ClickByPartialLinkTextUsingScript(identify);
                        break;
                    default:
                        sut.ClickByXPathUsingScript(identify);
                        break;
                }

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
                   .GoToUrl(url, out _);

                switch (type)
                {
                    case ElementsType.Id:
                        sut.ClickFirstById(identify);
                        break;
                    case ElementsType.Name:
                        sut.ClickFirstByName(identify);
                        break;
                    case ElementsType.TagName:
                        sut.ClickFirstByTagName(identify);
                        break;
                    case ElementsType.ClassName:
                        sut.ClickFirstByClassName(identify);
                        break;
                    case ElementsType.CssSelector:
                        sut.ClickFirstByCssSelector(identify);
                        break;
                    case ElementsType.LinkText:
                        sut.ClickFirstByLinkText(identify);
                        break;
                    case ElementsType.PartialLinkText:
                        sut.ClickFirstByPartialLinkText(identify);
                        break;
                    default:
                        sut.ClickFirstByXPath(identify);
                        break;
                }

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
                   .GetElementById(idNumberResult, out var checkElement);

                int.TryParse(checkElement.GetAttribute("value"), out var elementValueBefore);

                switch (elements)
                {
                    case ElementsType.Id:
                        sut.ClickByIdIfTrue(identify, condition);
                        break;
                    case ElementsType.Name:
                        sut.ClickByNameIfTrue(identify, condition);
                        break;
                    case ElementsType.TagName:
                        break;
                    case ElementsType.ClassName:
                        sut.ClickByClassNameIfTrue(identify, condition);
                        break;
                    case ElementsType.CssSelector:
                        sut.ClickByCssSelectorIfTrue(identify, condition);
                        break;
                    case ElementsType.LinkText:
                        break;
                    case ElementsType.PartialLinkText:
                        sut.ClickByPartialLinkTextIfTrue(identify, condition);
                        break;
                    default:
                        sut.ClickByXPathIfTrue(identify, condition);
                        break;
                }

                sut.GetElementById(idNumberResult, out checkElement);

                int.TryParse(checkElement.GetAttribute("value"), out var elementValueAfter);

                if (condition)
                    elementValueAfter.Should().BeGreaterThan(elementValueBefore);
                else
                    elementValueAfter.Should().Be(elementValueBefore);
            }
        }

        public static IEnumerable<object[]> GetElementsToClick() => new List<object[]> {
                new object[] { LocalUrl, "//*[@id='ButtonsToXPath']", ElementsType.XPath, false },
                new object[] { LocalUrl, "inputName", ElementsType.Name, false },
                new object[] { LocalUrl, "INPUT", ElementsType.TagName, false},
                new object[] { LocalUrl, "body > a", ElementsType.CssSelector, false},
                new object[] { LocalUrl, "btnOne", ElementsType.Id, false},
                new object[] { LocalUrl, "someClass", ElementsType.ClassName, false }
        };

        public static IEnumerable<object[]> GetElementsToClickWithCondition() => new List<object[]> {
            new object[] { LocalUrl,  "buttonIncrement", ElementsType.Id, "numberResult", true},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.Name, "numberResult", true},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.ClassName, "numberResult", true},
            new object[] { LocalUrl, "//*[@id='buttonIncrement']", ElementsType.XPath, "numberResult", true},
            new object[] { LocalUrl, "#buttonIncrement", ElementsType.CssSelector, "numberResult", true},
            new object[] { LocalUrl, "click to increment", ElementsType.PartialLinkText, "numberResult", true},
            new object[] { LocalUrl,  "buttonIncrement", ElementsType.Id, "numberResult", false},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.Name, "numberResult", false},
            new object[] { LocalUrl, "buttonIncrement", ElementsType.ClassName, "numberResult", false},
            new object[] { LocalUrl, "//*[@id='buttonIncrement']", ElementsType.XPath, "numberResult", false},
            new object[] { LocalUrl, "#buttonIncrement", ElementsType.CssSelector, "numberResult", false},
            new object[] { LocalUrl, "click to increment", ElementsType.PartialLinkText, "numberResult", false}
        };
    }
}
