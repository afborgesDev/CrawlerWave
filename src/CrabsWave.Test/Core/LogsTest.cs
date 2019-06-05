using CrabsWave.Core;
using CrabsWave.Core.LogsReports;
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
            LogManager.Initializate(loggerMoq.Object, false);
            LogManager.LogInformation(ExampleMessage);
            loggerMoq.VerifyLog(LogLevel.Information, ExampleMessage, Times.Never());
        }
    }
}
