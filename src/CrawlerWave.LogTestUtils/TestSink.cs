using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace CrawlerWave.LogTestUtils
{
    [ExcludeFromCodeCoverage]
    public class TestSink : ITestSink
    {
        private ConcurrentQueue<BeginScopeContext> _scopes;
        private ConcurrentQueue<WriteContext> _writes;

        public TestSink(
            Func<WriteContext, bool> writeEnabled = null,
            Func<BeginScopeContext, bool> beginEnabled = null)
        {
            WriteEnabled = writeEnabled;
            BeginEnabled = beginEnabled;

            _scopes = new ConcurrentQueue<BeginScopeContext>();
            _writes = new ConcurrentQueue<WriteContext>();
        }

        public event Action<WriteContext> MessageLogged;

        public event Action<BeginScopeContext> ScopeStarted;

        public Func<WriteContext, bool> WriteEnabled { get; set; }

        public Func<BeginScopeContext, bool> BeginEnabled { get; set; }

        public IProducerConsumerCollection<BeginScopeContext> Scopes { get => _scopes; set => _scopes = new ConcurrentQueue<BeginScopeContext>(value); }

        public IProducerConsumerCollection<WriteContext> Writes { get => _writes; set => _writes = new ConcurrentQueue<WriteContext>(value); }

        public static bool EnableWithTypeName<T>(WriteContext context) => context.LoggerName.Equals(typeof(T).FullName);

        public static bool EnableWithTypeName<T>(BeginScopeContext context) => context.LoggerName.Equals(typeof(T).FullName);

        public void Write(WriteContext context)
        {
            if (WriteEnabled == null || WriteEnabled(context))
                _writes.Enqueue(context);
            MessageLogged?.Invoke(context);
        }

        public void Begin(BeginScopeContext context)
        {
            if (BeginEnabled == null || BeginEnabled(context))
                _scopes.Enqueue(context);
            ScopeStarted?.Invoke(context);
        }
    }
}
