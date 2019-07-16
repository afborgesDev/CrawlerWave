using System.Collections.Generic;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrawlerWave.LogTestUtils;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerElementsTest
    {
        public static IEnumerable<object[]> GetElementToFind()
        {
            var url = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";
            return new List<object[]> {
                new object[] { url, "//*[@id='ButtonsToXPath']", ElementsType.XPath, true },
                new object[] { url, "inputName", ElementsType.Name, true },
                new object[] { url, "INPUT", ElementsType.TagName, true},
                new object[] { url, "body > a", ElementsType.CssSelector, true},
                new object[] { url, "btnOne", ElementsType.Id, true},
                new object[] { url, "someClass", ElementsType.ClassName, true },
                new object[] { url, "click to increment", ElementsType.PartialLinkText, true},
                new object[] { url, "click to increment" , ElementsType.LinkText, true},
                new object[] { url, "click to increment" , ElementsType.LinkText, false}
            };
        }

        [Theory]
        [MemberData(nameof(GetElementToFind))]
        public void ShouldFindObject(string url, string identify, ElementsType elementType, bool shouldRetry)
        {
            using (var sut = CreateAndInitialize())
            {
                sut.GoToUrl(url, out _)
                   .GetElement(identify, elementType, shouldRetry, out var element);

                element.Should().NotBeNull();
            }
        }

        [Theory]
        [MemberData(nameof(GetElementToFind))]
        public void ShouldGetElements(string url, string identify, ElementsType elementType, bool shouldRetry)
        {
            using (var sut = CreateAndInitialize())
            {
                sut.GoToUrl(url, out _)
                   .GetElements(identify, elementType, shouldRetry, out var elements);

                elements.Should().NotBeNull();
                elements.Should().NotBeEmpty();
            }
        }

        private Crawler CreateAndInitialize()
        {
            var (_, factory) = CreateForTest.Create();
            var sut = new Crawler(factory);
            sut.Initializate(new CrabsWave.Core.Configurations.Behavior());
            return sut;
        }
    }
}
