using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CrabsWave.Test.TestHelpers
{
    public static class TestLoggerBuilder
    {
        public static ILoggerFactory Create(Action<ILoggingBuilder> configure) => new ServiceCollection().AddLogging(configure).BuildServiceProvider().GetRequiredService<ILoggerFactory>();
        public static (ILogger<T>, TestTestOutputHelper) Create<T>(LogLevel logLevel = LogLevel.Trace)
        {
            var testOutputHelper = new TestTestOutputHelper();
            var logFactory = new ServiceCollection().AddLogging(x => x.SetMinimumLevel(logLevel).AddXunit(testOutputHelper)).BuildServiceProvider().GetRequiredService<ILoggerFactory>();
            return (logFactory.CreateLogger<T>(), testOutputHelper);
        }
    }
}
