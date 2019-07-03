using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
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
        Times times, string errorMessage = "") => logger.Verify(l => l.Log(logLevel, It.IsAny<EventId>(), It.Is<object>(o => MatchNear(o, message)), null,
                                                                It.IsAny<Func<object, Exception, string>>()), times, errorMessage);

        public static void VerifyMessage<T>(this Mock<ILogger<T>> logger,
            LogLevel logLevel, string message) => logger.Verify(
                l => l.Log(LogLevel.Error, 0, It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>())
                );


        //logger.Verify(l => l.Log(LogLevel.Error, message), Times.AtLeastOnce());

        public static bool MatchNear(object toTest, string message)
        {
            var msg = toTest.ToString();
            return msg.Contains(message, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
