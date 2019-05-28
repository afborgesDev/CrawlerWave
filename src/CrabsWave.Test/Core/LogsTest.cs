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
        public void ShouldntWriteLogInformation()
        {
            var loggerMoq = new Mock<ILogger<Crawler>>();
            var sut = new Crawler(loggerMoq.Object);
            sut.Loginformation(ExampleMessage);
            loggerMoq.VerifyLog(LogLevel.Information, ExampleMessage, Times.Never());
        }
    }
}
