using System.Collections.Generic;
using System.Linq;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrawlerWave.LogTestUtils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerRemoveTest
    {
        public static IEnumerable<object[]> GetElementToRemove() => new List<object[]> {
            new object[] { WebElementType.Id("numberResult"), true, null, ""},
            new object[] { WebElementType.ClassName("buttonIncrement"), true, null, ""},
            new object[] { WebElementType.Id("1numberResult1"), false, LogLevel.Error, "Could not get the element using identify: 1numberResult1"},
            new object[] { WebElementType.ClassName("1buttonIncrement1"), false, LogLevel.Error, "Could not get the element using identify: 1buttonIncrement1"},
            new object[] { WebElementType.XPath("1buttonIncrement1"), false, LogLevel.Information, "The WebElementType should be a ID or ClassName"},
            new object[] { null, false, LogLevel.Information, "An ID or ClassName are required to remove an element" }
        };

        [Theory]
        [MemberData(nameof(GetElementToRemove))]
        public void ShouldRemoveElement(WebElementType webElementType, bool shouldRemove, LogLevel logLevel, string logMessage)
        {
            var (logSilk, logMoq) = CreateForTest.Create();
            using (var sut = new Crawler(logMoq))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .RemoveElement(webElementType)
                   .GetElement(webElementType, false, out var element);

                if (shouldRemove)
                {
                    element.Should().BeNull();
                }
                else
                {
                    logSilk.Writes.Any(x => x.LogLevel == logLevel && x.Message.Contains(logMessage, System.StringComparison.InvariantCultureIgnoreCase))
                                  .Should().BeTrue();
                }
            }
        }
    }
}
