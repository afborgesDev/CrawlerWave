using System;
using System.Collections.Generic;
using System.Text;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerElementTextTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        [Theory]
        [MemberData(nameof(GetElementsToTestText))]
        public static void ShouldGetElementText(string identify, string expectedValue, ElementsType elementsType)
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logMoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _);
                string text = null;
                switch (elementsType)
                {
                    case ElementsType.Id:
                        sut.ElementTextById(identify, out text);
                        break;
                    case ElementsType.Name:
                        sut.ElementTextByName(identify, out text);
                        break;
                    case ElementsType.TagName:
                        sut.ElementTextByTagName(identify, out text);
                        break;
                    case ElementsType.ClassName:
                        sut.ElementTextByClassName(identify, out text);
                        break;
                    case ElementsType.CssSelector:
                        sut.ElementTextByCssSelector(identify, out text);
                        break;
                    case ElementsType.LinkText:
                        sut.ElementTextByLinkText(identify, out text);
                        break;
                    case ElementsType.PartialLinkText:
                        sut.ElementTextByPartialLinkText(identify, out text);
                        break;
                    default:
                        sut.ElementTextByXPath(identify, out text);
                        break;
                }

                text.Should().Be(expectedValue);
            }
        }

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
    }
}
