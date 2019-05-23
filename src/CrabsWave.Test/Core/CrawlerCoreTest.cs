using CrabsWave.Core;
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
            var sut = new Crawler(loggerMoq.Object);

            sut.Should().NotBeNull();
            loggerMoq.VerifyLog(LogLevel.Information, "Crawler created, starting to configure", Times.Once());
        }
    }
}
