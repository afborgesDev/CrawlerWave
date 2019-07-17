using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace CrawlerWave.LogTestUtils.Logger
{
    [ExcludeFromCodeCoverage]
    public class TestLoggerProvider : ILoggerProvider
    {
        private readonly Func<LogLevel, bool> _filter;

        public TestLoggerProvider(ITestSink testSink, bool isEnabled) :
            this(testSink, _ => isEnabled)
        {
        }

        public TestLoggerProvider(ITestSink testSink, Func<LogLevel, bool> filter)
        {
            Sink = testSink;
            _filter = filter;
        }

        public ITestSink Sink { get; }

        public ILogger CreateLogger(string categoryName) => new TestLogger(categoryName, Sink, _filter);

        #region IDisposable Support

        private bool disposedValue;

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        #endregion IDisposable Support
    }
}
