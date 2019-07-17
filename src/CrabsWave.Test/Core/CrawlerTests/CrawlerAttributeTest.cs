using System.Collections.Generic;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrawlerWave.LogTestUtils;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerAttributeTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        public static IEnumerable<object[]> GetItemsToTestAttribute() => new List<object[]> {
            new object[] { "numberResult", "value", "0", ElementsType.Id, false},
            new object[] { "numberResult", "value", "0", ElementsType.Name, false},
            new object[] { "P", "name", "paragraph", ElementsType.TagName, false},
            new object[] { "someClass", "name", "linkWithClass", ElementsType.ClassName, false},
            new object[] { "#buttonIncrement", "name", "buttonIncrement", ElementsType.CssSelector, false},
            new object[] { "click to increment", "name", "linkToIncrement", ElementsType.LinkText, false},
            new object[] { "click to increment", "name", "linkToIncrement", ElementsType.PartialLinkText, false},
            new object[] { "//*[@id='buttonIncrement']", "name", "buttonIncrement", ElementsType.XPath, false},
            new object[] { "//*[@id='buttonIncrement']", "invalidAttribute123", null, ElementsType.XPath, true},
            new object[] { "//*[@id='buttonIncrement']", "invalidAttribute123* \'as-=%78&*$%()'", null, ElementsType.XPath, true}
        };

        [Theory]
        [MemberData(nameof(GetItemsToTestAttribute))]
        public void ShouldGetElementAttribute(string identify, string attribute, string expectedValue, ElementsType elementsType, bool shouldRetry)
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .GetElementAttribute(identify, elementsType, attribute, shouldRetry, out var value);

                if (string.IsNullOrEmpty(expectedValue))
                    value.Should().BeNullOrWhiteSpace();
                else
                    value.Should().Be(expectedValue);
            }
        }

        [Fact]
        public void ShouldTestAttributeManager()
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GetElementAttribute("invalix", ElementsType.Id, "value", false, out var value);
                value.Should().BeNullOrWhiteSpace();
            }
        }
    }
}
