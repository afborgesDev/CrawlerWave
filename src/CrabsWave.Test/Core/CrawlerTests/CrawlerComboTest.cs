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
            new object[] { "select1", ElementsType.Id, "First Value", "first", false, false, string.Empty } ,
            new object[] { "select1", ElementsType.Name, "First Value", "first", false, false, string.Empty } ,
            new object[] { "select1", ElementsType.ClassName, "First Value", "first", false, false, string.Empty } ,
            new object[] { "select1", ElementsType.Id, "invalid 123", "-1", false, true, "Could not select element: select1 by using the text: invalid 123" } ,
            new object[] { "#$select1%¨&", ElementsType.Id, "invalid 123", "", false, true, "Could not find a select with the identify: #$select1%¨&" } ,
        };

        public static IEnumerable<object[]> GetSelectOptionsByValueToIdentify => new List<object[]> {
            new object[] { "select1", ElementsType.Id, "first", "first", false, false, string.Empty },
            new object[] { "select1", ElementsType.Name, "second", "second", false, false, string.Empty },
            new object[] { "select1", ElementsType.ClassName, "third", "third", false, false, string.Empty },
            new object[] { "select1", ElementsType.Id, "third2", "-1", false, true, "Could not select element: select1 by using the value: third2" },
            new object[] { "#$select1%¨&", ElementsType.Id, "third2", "", false, true, "Could not find a select with the identify: #$select1%¨&" },
        };

        public static IEnumerable<object[]> GetSelectOptionsByIndexToIdentify => new List<object[]> {
            new object[] { "select1", ElementsType.Id, 1, "first", false, false, string.Empty },
            new object[] { "select1", ElementsType.Name, 2, "second", false, false, string.Empty },
            new object[] { "select1", ElementsType.ClassName, 3, "third", false, false, string.Empty },
            new object[] { "select1", ElementsType.Id, 50, "-1", false, true, "Could not select element: select1 by using the index: 50" },
            new object[] { "#$select1%¨&", ElementsType.Id, -1, "", false, true, "Could not find a select with the identify: #$select1%¨&" },
        };

        [Theory]
        [MemberData(nameof(GetSelectOptionsToIdentify))]
        public void ShouldSelectByText(string identify, ElementsType elementsType, string textToSelect,
            string expectedValue, bool shouldRetry, bool shouldFail, string errorMessage)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .SelectByText(identify, elementsType, textToSelect, shouldRetry)
                   .GetElementAttribute(identify, elementsType, "value", shouldRetry, out var value);
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
        public void ShouldSelectByValue(string identify, ElementsType elementsType, string valueToSelect, string expectedValue, bool shouldRetry, bool shouldFail, string errorMessage)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .SelectByValue(identify, elementsType, valueToSelect, shouldRetry)
                   .GetElementAttribute(identify, elementsType, "value", shouldRetry, out var value);

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
        public void ShouldSelectByIndex(string identify, ElementsType elementsType, int indexToSelect, string expectedValue, bool shouldRetry, bool shouldFail, string errorMessage)
        {
            var (testSink, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .SelectByIndex(identify, elementsType, indexToSelect, shouldRetry)
                   .GetElementAttribute(identify, elementsType, "value", shouldRetry, out var value);

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
