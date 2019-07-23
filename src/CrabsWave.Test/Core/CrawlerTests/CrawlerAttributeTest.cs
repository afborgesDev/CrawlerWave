﻿using System.Collections.Generic;
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
            new object[] { WebElementType.Id("numberResult", false), "value", "0"},
            new object[] { WebElementType.Name("numberResult", false), "value", "0"},
            new object[] { WebElementType.TagName("P", false), "name", "paragraph"},
            new object[] { WebElementType.ClassName("someClass", false), "name", "linkWithClass"},
            new object[] { WebElementType.CssSelector("#buttonIncrement", false), "name", "buttonIncrement"},
            new object[] { WebElementType.LinkText("click to increment", false), "name", "linkToIncrement"},
            new object[] { WebElementType.PartialLinkText("click to increment", false), "name", "linkToIncrement"},
            new object[] { WebElementType.XPath("//*[@id='buttonIncrement']", false), "name", "buttonIncrement"},
            new object[] { WebElementType.XPath("//*[@id='buttonIncrement']", true), "invalidAttribute123", null},
            new object[] { WebElementType.XPath("//*[@id='buttonIncrement']", true), "invalidAttribute123* \'as-=%78&*$%()'", null}
        };

        [Theory]
        [MemberData(nameof(GetItemsToTestAttribute))]
        public void ShouldGetElementAttribute(WebElementType webElementType, string attribute, string expectedValue)
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .GetElementAttribute(webElementType, attribute,  out var value);

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
                   .GetElementAttribute(WebElementType.Id("invalix"), "value", out var value);
                value.Should().BeNullOrWhiteSpace();
            }
        }
    }
}
