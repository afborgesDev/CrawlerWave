using CrabsWave.Core;
using CrabsWave.Core.Configurations;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core
{
    public class CrawlerCoreTest
    {
        [Fact]
        public void ShouldCreateWithDefaultParameters()
        {
            var loggerMoq = new Mock<ILogger<Crawler>>();
            var sut = new Crawler(new Behavior(), loggerMoq.Object);

            sut.Should().NotBeNull();
            loggerMoq.VerifyLog(LogLevel.Information, "Crawler created, starting to configure", Times.Once());
        }
    }
}
