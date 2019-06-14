using System.Collections.Generic;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerClickTest
    {
        private const string GoogleBaseUrl = "https://www.google.com.br";

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

        public static IEnumerable<object[]> GetElementsToClick() => new List<object[]> {
            new object[] { GoogleBaseUrl, "//*[@id='tsf']/div[2]/div/div[3]/center/input[2]", ElementsType.XPath, false },
            new object[] { GoogleBaseUrl, "btnI", ElementsType.Name, false },
            new object[] { GoogleBaseUrl, "INPUT", ElementsType.TagName, false},
            new object[] { GoogleBaseUrl, "//*/div[2]/div/div[3]/center/input[2]" , ElementsType.CssSelector, false},
            new object[] { GoogleBaseUrl, "gb_70", ElementsType.Id, false}
        };
    }
}
