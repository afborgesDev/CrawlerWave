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
            new object[] { WebElementType.Id("numberResult"), "value", "0", false},
            new object[] { WebElementType.Name("numberResult"), "value", "0", false},
            new object[] { WebElementType.TagName("P"), "name", "paragraph", false},
            new object[] { WebElementType.ClassName("someClass"), "name", "linkWithClass", false},
            new object[] { WebElementType.CssSelector("#buttonIncrement"), "name", "buttonIncrement", false},
            new object[] { WebElementType.LinkText("click to increment"), "name", "linkToIncrement", false},
            new object[] { WebElementType.PartialLinkText("click to increment"), "name", "linkToIncrement", false},
            new object[] { WebElementType.XPath("//*[@id='buttonIncrement']"), "name", "buttonIncrement", false},
            new object[] { WebElementType.XPath("//*[@id='buttonIncrement']"), "invalidAttribute123", null, true},
            new object[] { WebElementType.XPath("//*[@id='buttonIncrement']"), "invalidAttribute123* \'as-=%78&*$%()'", null, true}
        };

        [Theory]
        [MemberData(nameof(GetItemsToTestAttribute))]
        public void ShouldGetElementAttribute(WebElementType webElementType, string attribute, string expectedValue, bool shouldRetry)
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .GetElementAttribute(webElementType, attribute, shouldRetry, out var value);

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
                   .GetElementAttribute(WebElementType.Id("invalix"), "value", false, out var value);
                value.Should().BeNullOrWhiteSpace();
            }
        }
    }
}
