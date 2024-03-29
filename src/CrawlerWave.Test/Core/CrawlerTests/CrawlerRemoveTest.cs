﻿using System.Collections.Generic;
using System.Linq;
using CrawlerWave.Core;
using CrawlerWave.Core.Configurations;
using CrawlerWave.Core.Resources;
using CrawlerWave.LogTestHelper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CrawlerWave.Test.Core.CrawlerTests
{
    public class CrawlerRemoveTest
    {
        public static IEnumerable<object[]> GetElementToRemove() => new List<object[]> {
            new object[] { WebElementType.Id("numberResult"), true, null, ""},
            new object[] { WebElementType.ClassName("buttonIncrement"), true, null, ""},
            new object[] { WebElementType.Id("1numberResult1"), false, LogLevel.Error, "Could not get the element using identify: 1numberResult1"},
            //new object[] { WebElementType.ClassName("1buttonIncrement1"), false, LogLevel.Error, "Could not get the element using identify: 1buttonIncrement1"},
            new object[] { WebElementType.XPath("1buttonIncrement1"), false, LogLevel.Information, "The WebElementType should be a ID or ClassName"},
            new object[] { null, false, LogLevel.Information, "An ID or ClassName are required to remove an element" }
        };

        [Theory]
        [MemberData(nameof(GetElementToRemove))]
        public void ShouldRemoveElement(WebElementType webElementType, bool shouldRemove, LogLevel logLevel, string logMessage)
        {
            var (logSilk, logMoq) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(logMoq))
            {
                sut.Initializate(new Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .RemoveElement(webElementType)
                   .GetElement(webElementType, out var element);

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
