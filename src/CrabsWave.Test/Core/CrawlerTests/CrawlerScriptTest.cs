using CrabsWave.Core;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerScriptTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";

        [Fact]
        public void ShouldExecuteScript()
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var crawler = new Crawler(logMoq.Object))
            {
                crawler.Initializate(new CrabsWave.Core.Configurations.Behavior())
                       .GoToUrl(LocalUrl, out _)
                       .ExecuteJavaScript("document.getElementById(\"buttonIncrement\").click()")
                       .GetElementById("numberResult", out var webElement);

                int.TryParse(webElement.GetAttribute("value"), out var value);
                value.Should().BeGreaterThan(0);
            }
        }

        [Fact]
        public void ShouldExecuteScriptWithParams()
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var crawler = new Crawler(logMoq.Object))
            {
                crawler.Initializate(new CrabsWave.Core.Configurations.Behavior())
                       .GoToUrl(LocalUrl, out _)
                       .GetElementById("buttonIncrement", out var buttonIncrement)
                       .ExecuteJavaScript("(arguments[0] || {click:() => ''}).click();", buttonIncrement)
                       .GetElementById("numberResult", out var webElement);

                int.TryParse(webElement.GetAttribute("value"), out var value);
                value.Should().BeGreaterThan(0);
            }
        }

        [Fact]
        public void ShouldExecuteScriptAndReturn()
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var crawler = new Crawler(logMoq.Object))
            {
                crawler.Initializate(new CrabsWave.Core.Configurations.Behavior())
                       .ExecuteJavaScript("return document.URL;", out var scriptResult);

                scriptResult.Should().NotBeNullOrWhiteSpace();
            }
        }
    }
}
