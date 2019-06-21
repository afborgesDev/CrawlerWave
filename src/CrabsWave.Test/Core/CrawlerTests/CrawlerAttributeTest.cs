using System.Collections.Generic;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerAttributeTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        [Theory]
        [MemberData(nameof(GetItemsToTestAttribute))]
        public void ShouldGetElementAttribute(string identify, string attribute, string expectedValue, ElementsType elementsType, bool shouldRetry)
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logMoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _);

                string value = null;
                switch (elementsType)
                {
                    case ElementsType.Id:
                        sut.GetElementAttributeById(identify, attribute, out value);
                        break;
                    case ElementsType.Name:
                        sut.GetElementAttributeByName(identify, attribute, out value);
                        break;
                    case ElementsType.TagName:
                        sut.GetElementAttributeByTagName(identify, attribute, out value);
                        break;
                    case ElementsType.ClassName:
                        sut.GetElementAttributeByClassName(identify, attribute, out value);
                        break;
                    case ElementsType.CssSelector:
                        sut.GetElementAttributeByCssSelector(identify, attribute, out value);
                        break;
                    case ElementsType.LinkText:
                        sut.GetElementAttributeByLinkText(identify, attribute, out value);
                        break;
                    case ElementsType.PartialLinkText:
                        sut.GetElementAttributeByPartialLinkText(identify, attribute, out value);
                        break;                    
                    default:
                        sut.GetElementAttributeByXPath(identify, attribute, out value);
                        break;
                }

                value.Should().Be(expectedValue);
            }
        }

        [Fact]
        public void ShouldTestAttributeManager()
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logMoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GetElementAttributeById("invalix", "value", out var value);
                value.Should().BeNullOrWhiteSpace();

            }
        }

        public static IEnumerable<object[]> GetItemsToTestAttribute() => new List<object[]> {
            new object[] { "numberResult", "value", "0", ElementsType.Id, false},
            new object[] { "numberResult", "value", "0", ElementsType.Name, false},
            new object[] { "P", "name", "paragraph", ElementsType.TagName, false},
            new object[] { "someClass", "name", "linkWithClass", ElementsType.ClassName, false},
            new object[] { "#buttonIncrement", "name", "buttonIncrement", ElementsType.CssSelector, false},
            new object[] { "click to increment", "name", "linkToIncrement", ElementsType.LinkText, false},
            new object[] { "click to increment", "name", "linkToIncrement", ElementsType.PartialLinkText, false},
            new object[] { "//*[@id='buttonIncrement']", "name", "buttonIncrement", ElementsType.XPath, false}
        };
    }
}
