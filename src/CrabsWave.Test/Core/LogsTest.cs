using CrabsWave.Core;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core
{
    public class LogsTest
    {
        private const string ExampleMessage = "Example Message";

        [Fact]
        public void ShouldWriteLogInformation()
        {
            var loggerMoq = new Mock<ILogger<Crawler>>();
            var sut = new Crawler(loggerMoq.Object);
            sut.Initializate(new CrabsWave.Core.Configurations.Behavior() { Verbose = true });

            sut.LoginformationAsync(ExampleMessage);
            loggerMoq.VerifyLog(LogLevel.Information, ExampleMessage, Times.Once());
        }

        [Fact]
        public void ShouldntWriteLogInformation()
        {
            var loggerMoq = new Mock<ILogger<Crawler>>();
            var sut = new Crawler(loggerMoq.Object);
            sut.Initializate(new CrabsWave.Core.Configurations.Behavior() { Verbose = false });

            sut.LoginformationAsync(ExampleMessage);
            loggerMoq.VerifyLog(LogLevel.Information, ExampleMessage, Times.Never());
        }
    }
}
