using System.Collections.Generic;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrawlerWave.LogTestUtils;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
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
            var (_, logMoq) = CreateForTest.Create();
            using (var sut = new Crawler(logMoq))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .GetElementAttribute(webElementType, "value", false, out var beforeValue)
                   .MouseMove(webElementType, false)
                   .GetElementAttribute(webElementType, "value", false, out var afterValue);

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
            var (_, logMoq) = CreateForTest.Create();
            using (var sut = new Crawler(logMoq))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .GetElementAttribute(checkElement, "value", false, out var beforeValue)
                   .MouseMoveAndClick(webElementType, false)
                   .GetElementAttribute(checkElement, "value", false, out var afterValue);

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
