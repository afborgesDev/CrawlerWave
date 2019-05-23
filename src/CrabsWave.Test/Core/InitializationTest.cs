using CrabsWave.Core;
using CrabsWave.Core.Configurations;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core
{
    public class InitializationTest
    {
        [Fact]
        public void ShouldNonGridInitilizationHasWeDriver()
        {

            var loggerMoq = new Mock<ILogger<ICrawler>>();
            var sut = new Crawler(loggerMoq.Object);
            sut.Initializate(new Behavior() { Verbose = true });

            loggerMoq.VerifyLog(LogLevel.Information, "Successful crab initilization", Times.Once());
        }

        [Fact]
        public void ShouldFailNoGridInitilizationWithNoWeDriver()
        {
            var loggerMoq = new Mock<ILogger<ICrawler>>();
            var sut = new Crawler(loggerMoq.Object);
            sut.Initializate(new Behavior() { Verbose = true });

            loggerMoq.VerifyLog(LogLevel.Error, "Could not initilization, missing webdriver", Times.Once());
        }
    }
}
