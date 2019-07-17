using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace CrawlerWave.LogTestUtils
{
    [ExcludeFromCodeCoverage]
    public class WriteContext
    {
        public LogLevel LogLevel { get; set; }

        public EventId EventId { get; set; }

        public object State { get; set; }

        public Exception Exception { get; set; }

        public Func<object, Exception, string> Formatter { get; set; }

        public object Scope { get; set; }

        public string LoggerName { get; set; }

        public string Message => Formatter(State, Exception);
    }
}
