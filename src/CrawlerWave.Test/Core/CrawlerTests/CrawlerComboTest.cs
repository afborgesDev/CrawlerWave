using System;
using System.Collections.Generic;
using System.Linq;
using CrawlerWave.Core;
using CrawlerWave.Core.Configurations;
using CrawlerWave.Core.Resources;
using CrawlerWave.LogTestHelper;
using FluentAssertions;
using Xunit;

namespace CrawlerWave.Test.Core.CrawlerTests
{
    public class CrawlerComboTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        public static IEnumerable<object[]> GetSelectOptionsToIdentify => new List<object[]> {
            new object[] { WebElementType.Id("select1", false), "First Value", "first", false, string.Empty } ,
            new object[] { WebElementType.Name("select1", false), "First Value", "first", false, string.Empty } ,
            new object[] { WebElementType.ClassName("select1", false), "First Value", "first", false, string.Empty } ,
            new object[] { WebElementType.Id("select1", false), "invalid 123", "-1", true, "Could not select element: select1 by using the text: invalid 123" } ,
            new object[] { WebElementType.Id("#$select1%¨&", false), "invalid 123", "", true, "Could not find a select with the identify: #$select1%¨&" } ,
        };

        public static IEnumerable<object[]> GetSelectOptionsByValueToIdentify => new List<object[]> {
            new object[] { WebElementType.Id("select1", false), "first", "first", false, string.Empty },
            new object[] { WebElementType.Name("select1", false), "second", "second", false, string.Empty },
            new object[] { WebElementType.ClassName("select1", false), "third", "third", false, string.Empty },
            new object[] { WebElementType.Id("select1", false), "third2", "-1", true, "Could not select element: select1 by using the value: third2" },
            new object[] { WebElementType.Id("#$select1%¨&", false), "third2", "", true, "Could not find a select with the identify: #$select1%¨&" },
        };

        public static IEnumerable<object[]> GetSelectOptionsByIndexToIdentify => new List<object[]> {
            new object[] { WebElementType.Id("select1", false), 1, "first", false, string.Empty },
            new object[] { WebElementType.Name("select1", false), 2, "second", false, string.Empty },
            new object[] { WebElementType.ClassName("select1", false), 3, "third", false, string.Empty },
            new object[] { WebElementType.Id("select1", false), 50, "-1", true, "Could not select element: select1 by using the index: 50" },
            new object[] { WebElementType.Id("#$select1%¨&", false), -1, "", true, "Could not find a select with the identify: #$select1%¨&" },
        };

        [Theory]
        [MemberData(nameof(GetSelectOptionsToIdentify))]
        public void ShouldSelectByText(WebElementType webElementType, string textToSelect,
            string expectedValue, bool shouldFail, string errorMessage)
        {
            var (testSink, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .SelectByText(webElementType, textToSelect)
                   .GetElementAttribute(webElementType, "value", out var value);
                value.Should().Be(expectedValue);

                if (shouldFail)
                {
                    _ = testSink.Writes.Any(x => x.Message.Contains(errorMessage,
                                                StringComparison.InvariantCultureIgnoreCase))
                            .Should().BeTrue();
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetSelectOptionsByValueToIdentify))]
        public void ShouldSelectByValue(WebElementType webElementType, string valueToSelect, string expectedValue,
            bool shouldFail, string errorMessage)
        {
            var (testSink, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .SelectByValue(webElementType, valueToSelect)
                   .GetElementAttribute(webElementType, "value", out var value);

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
        public void ShouldSelectByIndex(WebElementType webElementType, int indexToSelect, string expectedValue,
            bool shouldFail, string errorMessage)
        {
            var (testSink, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .SelectByIndex(webElementType, indexToSelect)
                   .GetElementAttribute(webElementType, "value", out var value);

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
