using System;
using System.Collections.Generic;
using System.Linq;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrawlerWave.LogTestUtils;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerComboTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        public static IEnumerable<object[]> GetSelectOptionsToIdentify => new List<object[]> {
            new object[] { WebElementType.Id("select1"), "First Value", "first", false, false, string.Empty } ,
            new object[] { WebElementType.Name("select1"), "First Value", "first", false, false, string.Empty } ,
            new object[] { WebElementType.ClassName("select1"), "First Value", "first", false, false, string.Empty } ,
            new object[] { WebElementType.Id("select1"), "invalid 123", "-1", false, true, "Could not select element: select1 by using the text: invalid 123" } ,
            new object[] { WebElementType.Id("#$select1%¨&"), "invalid 123", "", false, true, "Could not find a select with the identify: #$select1%¨&" } ,
        };

        public static IEnumerable<object[]> GetSelectOptionsByValueToIdentify => new List<object[]> {
            new object[] { WebElementType.Id("select1"), "first", "first", false, false, string.Empty },
            new object[] { WebElementType.Name("select1"), "second", "second", false, false, string.Empty },
            new object[] { WebElementType.ClassName("select1"), "third", "third", false, false, string.Empty },
            new object[] { WebElementType.Id("select1"), "third2", "-1", false, true, "Could not select element: select1 by using the value: third2" },
            new object[] { WebElementType.Id("#$select1%¨&"), "third2", "", false, true, "Could not find a select with the identify: #$select1%¨&" },
        };

        public static IEnumerable<object[]> GetSelectOptionsByIndexToIdentify => new List<object[]> {
            new object[] { WebElementType.Id("select1"), 1, "first", false, false, string.Empty },
            new object[] { WebElementType.Name("select1"), 2, "second", false, false, string.Empty },
            new object[] { WebElementType.ClassName("select1"), 3, "third", false, false, string.Empty },
            new object[] { WebElementType.Id("select1"), 50, "-1", false, true, "Could not select element: select1 by using the index: 50" },
            new object[] { WebElementType.Id("#$select1%¨&"), -1, "", false, true, "Could not find a select with the identify: #$select1%¨&" },
        };

        [Theory]
        [MemberData(nameof(GetSelectOptionsToIdentify))]
        public void ShouldSelectByText(WebElementType webElementType, string textToSelect,
            string expectedValue, bool shouldRetry, bool shouldFail, string errorMessage)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .SelectByText(webElementType, textToSelect, shouldRetry)
                   .GetElementAttribute(webElementType, "value", shouldRetry, out var value);
                value.Should().Be(expectedValue);

                if (shouldFail)
                {
                    testSink.Writes.Any(x => x.Message.Contains(errorMessage,
                                              StringComparison.InvariantCultureIgnoreCase))
                            .Should().BeTrue();
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetSelectOptionsByValueToIdentify))]
        public void ShouldSelectByValue(WebElementType webElementType, string valueToSelect, string expectedValue, bool shouldRetry, bool shouldFail, string errorMessage)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .SelectByValue(webElementType, valueToSelect, shouldRetry)
                   .GetElementAttribute(webElementType, "value", shouldRetry, out var value);

                value.Should().Be(expectedValue);
                if (shouldFail)
                {
                    testSink.Writes.Any(x => x.Message.Contains(errorMessage,
                                              StringComparison.InvariantCultureIgnoreCase))
                            .Should().BeTrue();
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetSelectOptionsByIndexToIdentify))]
        public void ShouldSelectByIndex(WebElementType webElementType, int indexToSelect, string expectedValue, bool shouldRetry, bool shouldFail, string errorMessage)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .SelectByIndex(webElementType, indexToSelect, shouldRetry)
                   .GetElementAttribute(webElementType, "value", shouldRetry, out var value);

                value.Should().Be(expectedValue);
                if (shouldFail)
                {
                    testSink.Writes.Any(x => x.Message.Contains(errorMessage,
                                              StringComparison.InvariantCultureIgnoreCase))
                            .Should().BeTrue();
                }
            }
        }
    }
}
