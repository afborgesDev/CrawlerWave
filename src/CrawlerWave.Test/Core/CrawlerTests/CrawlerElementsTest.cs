using System.Collections.Generic;
using CrawlerWave.Core;
using CrawlerWave.Core.Configurations;
using CrawlerWave.Core.Resources;
using CrawlerWave.LogTestHelper;
using FluentAssertions;
using Xunit;

namespace CrawlerWave.Test.Core.CrawlerTests
{
    public class CrawlerElementsTest
    {
        public static IEnumerable<object[]> GetElementToFind()
        {
            var url = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";
            return new List<object[]> {
                new object[] { url, WebElementType.XPath("//*[@id='ButtonsToXPath']", true) },
                new object[] { url, WebElementType.Name("inputName", true),},
                new object[] { url, WebElementType.TagName("INPUT", true)},
                new object[] { url, WebElementType.CssSelector("body > a", true)},
                new object[] { url, WebElementType.Id("btnOne", true)},
                new object[] { url, WebElementType.ClassName("someClass", true)},
                new object[] { url, WebElementType.PartialLinkText("click to increment", true)},
                new object[] { url, WebElementType.LinkText("click to increment", true)},
                new object[] { url, WebElementType.LinkText("click to increment", false)}
            };
        }

        [Theory]
        [MemberData(nameof(GetElementToFind))]
        public void ShouldFindObject(string url, WebElementType webElementType)
        {
            using (var sut = CreateAndInitialize())
            {
                sut.GoToUrl(url, out _)
                   .GetElement(webElementType, out var element);

                element.Should().NotBeNull();
            }
        }

        [Theory]
        [MemberData(nameof(GetElementToFind))]
        public void ShouldGetElements(string url, WebElementType webElementType)
        {
            using (var sut = CreateAndInitialize())
            {
                sut.GoToUrl(url, out _)
                   .GetElements(webElementType, out var elements);

                elements.Should().NotBeNull();
                elements.Should().NotBeEmpty();
            }
        }

        private Crawler CreateAndInitialize()
        {
            var (_, factory) = LogTestHelperInitialization.Create();
            var sut = new Crawler(factory);
            sut.Initializate(new Behavior());
            return sut;
        }
    }
}
