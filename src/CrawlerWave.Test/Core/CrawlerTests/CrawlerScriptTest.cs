using System.Linq;
using CrawlerWave.Core;
using CrawlerWave.Core.Configurations;
using CrawlerWave.Core.Resources;
using CrawlerWave.LogTestHelper;
using FluentAssertions;
using Xunit;

namespace CrawlerWave.Test.Core.CrawlerTests
{
    public class CrawlerScriptTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        [Fact]
        public void ShouldExecuteScript()
        {
            var (_, factory) = LogTestHelperInitialization.Create();
            using (var crawler = new Crawler(factory))
            {
                crawler.Initializate(new Behavior())
                       .GoToUrl(LocalUrl, out _)
                       .ExecuteJavaScript("document.getElementById(\"buttonIncrement\").click()")
                       .GetElement(WebElementType.Id("numberResult", true), out var webElement);

                int.TryParse(webElement.GetAttribute("value"), out var value);
                value.Should().BeGreaterThan(0);
            }
        }

        [Fact]
        public void ShouldExecuteScriptWithParams()
        {
            var (_, factory) = LogTestHelperInitialization.Create();
            using (var crawler = new Crawler(factory))
            {
                crawler.Initializate(new Behavior())
                       .GoToUrl(LocalUrl, out _)
                       .GetElement(WebElementType.Id("buttonIncrement", true), out var buttonIncrement)
                       .ExecuteJavaScript("(arguments[0] || {click:() => ''}).click();", buttonIncrement)
                       .GetElement(WebElementType.Id("numberResult", true), out var webElement);

                int.TryParse(webElement.GetAttribute("value"), out var value);
                value.Should().BeGreaterThan(0);
            }
        }

        [Fact]
        public void ShouldExecuteScriptAndReturn()
        {
            var (_, factory) = LogTestHelperInitialization.Create();
            using (var crawler = new Crawler(factory))
            {
                crawler.Initializate(new Behavior())
                       .ExecuteJavaScript("return document.URL;", out var scriptResult);

                scriptResult.Should().NotBeNullOrWhiteSpace();
            }
        }

        [Fact]
        public void ShouldLogErrorOnExecuteScript()
        {
            var (sink, factory) = LogTestHelperInitialization.Create();
            using (var crawler = new Crawler(factory))
            {
                crawler.Initializate(new Behavior { Verbose = true })
                       .ExecuteJavaScript("arguments[0].click();", "myelement");

                sink.Writes.Any(x =>
                    x.Message.Contains("Could not execute javascript using args and JavaScriptExecutor engine",
                    System.StringComparison.InvariantCultureIgnoreCase)).Should().BeTrue();
            }
        }

        [Fact]
        public void ShouldLogErrorOnExecuteScriptToTakeResult()
        {
            var (sink, factory) = LogTestHelperInitialization.Create();
            using (var crawler = new Crawler(factory))
            {
                crawler.Initializate(new Behavior())
                       .ExecuteJavaScript("arguments[0].click();", out var result);

                sink.Writes.Any(x =>
                    x.Message.Contains("Could not execute javascript and take a result",
                    System.StringComparison.InvariantCultureIgnoreCase)).Should().BeTrue();
            }
        }
    }
}
