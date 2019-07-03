using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CrabsWave.Test.TestHelpers
{
    public static class TestLoggerBuilder
    {
        public static ILoggerFactory Create(Action<ILoggingBuilder> configure) => new ServiceCollection().AddLogging(configure).BuildServiceProvider().GetRequiredService<ILoggerFactory>();
    }
}
