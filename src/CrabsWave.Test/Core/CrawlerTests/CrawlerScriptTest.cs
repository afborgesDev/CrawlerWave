using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrabsWave.Test.TestHelpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerScriptTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";
        private readonly ITestOutputHelper testOutput;

        public CrawlerScriptTest(ITestOutputHelper output) => testOutput = output;

        [Fact]
        public void ShouldExecuteScript()
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var crawler = new Crawler(logMoq.Object))
            {
                crawler.Initializate(new CrabsWave.Core.Configurations.Behavior())
                       .GoToUrl(LocalUrl, out _)
                       .ExecuteJavaScript("document.getElementById(\"buttonIncrement\").click()")
                       .GetElement("numberResult", ElementsType.Id, out var webElement);

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
                       .GetElement("buttonIncrement", ElementsType.Id, out var buttonIncrement)
                       .ExecuteJavaScript("(arguments[0] || {click:() => ''}).click();", buttonIncrement)
                       .GetElement("numberResult", ElementsType.Id, out var webElement);

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

        [Fact]
        public void ShouldLogErrorOnExecuteScript()
        {
            var (logMoq, logOutPut) = TestLoggerBuilder.Create<Crawler>();
            testOutput.WriteLine($"the logmoq are null?: {logMoq == null}");
            using (var crawler = new Crawler(logMoq))
            {
                crawler.Initializate(new CrabsWave.Core.Configurations.Behavior { Verbose = true })
                       .ExecuteJavaScript("arguments[0].click();", "myelement");

                testOutput.WriteLine(logOutPut.Output);
                logOutPut.Output.Contains("Could not execute javascript using args and JavaScriptExecutor engine").Should().BeTrue();
            }
        }

        [Fact]
        public void ShouldLogErrorOnExecuteScriptToTakeResult()
        {
            var (logMoq, logOutPut) = TestLoggerBuilder.Create<Crawler>();
            using (var crawler = new Crawler(logMoq))
            {
                crawler.Initializate(new CrabsWave.Core.Configurations.Behavior())
                       .ExecuteJavaScript("arguments[0].click();", out var result);

                testOutput.WriteLine(logOutPut.Output);
                logOutPut.Output.Contains("Could not execute javascript and take a result").Should().BeTrue();
            }
        }
    }
}
