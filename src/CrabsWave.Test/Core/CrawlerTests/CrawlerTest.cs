using CrabsWave.Core;
using CrabsWave.Core.Configurations;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerTest
    {
        [Fact]
        public void ShouldInitializateCrawler()
        {
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new Behavior());
                sut.Ready.Should().BeTrue();
            }
        }

        [Fact]
        public void ShouldNotLog()
        {
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                var behavior = new Behavior() { Verbose = false };
                sut.Initializate(behavior);
                behavior.Verbose.Should().BeFalse();
                logmoq.VerifyLog(LogLevel.Information, "Crawler created, starting to configure", Times.AtLeastOnce());
                logmoq.VerifyLog(LogLevel.Information, "The Crawler is ready to use.", Times.Never());
            }
        }
    }
}
