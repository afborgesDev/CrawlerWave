using System.Diagnostics.CodeAnalysis;

namespace CrawlerWave.LogTestUtils
{
    [ExcludeFromCodeCoverage]
    public class BeginScopeContext
    {
        public object Scope { get; set; }

        public string LoggerName { get; set; }
    }
}
