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
            var sut = new Crawler(new CrabsWave.Core.Configurations.Behavior() { Verbose = true }, loggerMoq.Object);

            sut.LoginformationAsync(ExampleMessage);
            loggerMoq.VerifyLog(LogLevel.Information, ExampleMessage, Times.Once());
        }

        [Fact]
        public void ShouldntWriteLogInformation()
        {
            var loggerMoq = new Mock<ILogger<Crawler>>();
            var sut = new Crawler(new CrabsWave.Core.Configurations.Behavior() { Verbose = false }, loggerMoq.Object);

            sut.LoginformationAsync(ExampleMessage);
            loggerMoq.VerifyLog(LogLevel.Information, ExampleMessage, Times.Never());
        }
    }
}
