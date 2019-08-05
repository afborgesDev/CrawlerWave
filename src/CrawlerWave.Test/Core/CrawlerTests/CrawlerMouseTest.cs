using System.Collections.Generic;
using CrawlerWave.Core;
using CrawlerWave.Core.Configurations;
using CrawlerWave.Core.Resources;
using CrawlerWave.LogTestHelper;
using FluentAssertions;
using Xunit;

namespace CrawlerWave.Test.Core.CrawlerTests
{
    public class CrawlerMouseTest
    {
        public static IEnumerable<object[]> GetElementsToMouseMove() => new List<object[]> {
            new object[]{ WebElementType.Id("textToMouse"), true},
            new object[]{ WebElementType.Id("textToMouse123"), false}
        };

        public static IEnumerable<object[]> GetElementToMoveAndClick() => new List<object[]> {
            new object[] { WebElementType.Id("buttonIncrement"), WebElementType.Id("numberResult"), true},
            new object[] { WebElementType.Id("buttonIncrement123"), WebElementType.Id("numberResult"), false}
        };

        [Theory]
        [MemberData(nameof(GetElementsToMouseMove))]
        public void MouseShouldMove(WebElementType webElementType, bool shouldMove)
        {
            var (_, logMoq) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(logMoq))
            {
                sut.Initializate(new Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .GetElementAttribute(webElementType, "value", out var beforeValue)
                   .MouseMove(webElementType)
                   .GetElementAttribute(webElementType, "value", out var afterValue);

                int.TryParse(beforeValue, out var beforeInt);
                int.TryParse(afterValue, out var afterInt);

                if (shouldMove)
                {
                    beforeInt.Should().Be(0);
                    afterInt.Should().BeGreaterThan(beforeInt);
                }
                else
                {
                    beforeInt.Should().Be(0);
                    afterInt.Should().Be(0);
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetElementToMoveAndClick))]
        public void MouseShouldClick(WebElementType webElementType, WebElementType checkElement, bool shouldMove)
        {
            var (_, logMoq) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(logMoq))
            {
                sut.Initializate(new Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .GetElementAttribute(checkElement, "value", out var beforeValue)
                   .MouseMoveAndClick(webElementType)
                   .GetElementAttribute(checkElement, "value", out var afterValue);

                int.TryParse(beforeValue, out var beforeInt);
                int.TryParse(afterValue, out var afterInt);

                if (shouldMove)
                {
                    beforeInt.Should().Be(0);
                    afterInt.Should().BeGreaterThan(beforeInt);
                }
                else
                {
                    beforeInt.Should().Be(0);
                    afterInt.Should().Be(0);
                }
            }
        }
    }
}
