using System.Runtime.InteropServices;
using Xunit;

namespace CrawlerWave.Test.Utils
{
    internal class TheoryIgnoreOnLinuxRunnerAttribute : TheoryAttribute
    {
        public TheoryIgnoreOnLinuxRunnerAttribute()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Skip = "Ignore on Linux runner";
            }
        }
    }
}
