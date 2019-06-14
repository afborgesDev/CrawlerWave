using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace CrabsWave.Test.Core
{
    public static class LogMoqHelper
    {
        public static void VerifyLog<T>(this Mock<ILogger<T>> logger,
            LogLevel logLevel,
            string message, Times times,
            string errorMessage = "") => logger.Verify(l => l.Log(logLevel, It.IsAny<EventId>(), It.Is<object>(o => o.ToString() == message), null,
                                                       It.IsAny<Func<object, Exception, string>>()), times, errorMessage);

        public static void VerifyNearLog<T>(this Mock<ILogger<T>> logger,
        LogLevel logLevel, string message, 
        Times times, string errorMessage = "") => logger.Verify(l => l.Log(logLevel, It.IsAny<EventId>(), It.Is<object>(o => o.ToString().Contains(message, StringComparison.InvariantCultureIgnoreCase)), null,
                                                                It.IsAny<Func<object, Exception, string>>()), times, errorMessage);
    }
}
