using CrabsWave.Core;
using CrabsWave.Core.Configurations;
using CrawlerWave.LogTestHelper;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerTest
    {
        [Fact]
        public void ShouldInitializateCrawler()
        {
            var (_, factory) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new Behavior());
                sut.Ready.Should().BeTrue();
            }
        }

        //Need to come with this code back
        //[Fact]
        //public void ShouldNotLog()
        //{
        //    var (testSink, factory) = LogTestHelperInitialization.Create();
        //    using (var sut = new Crawler(factory))
        //    {
        //        var behavior = new Behavior() { Verbose = false };
        //        sut.Initializate(behavior);
        //        behavior.Verbose.Should().BeFalse();

        // testSink.Writes.Any(x => x.LogLevel == LogLevel.Information && x.Message.Contains("Crawler
        // created, starting to configure", System.StringComparison.InvariantCultureIgnoreCase)) .Should().BeTrue();

        //        testSink.Writes.Any(x => x.LogLevel == LogLevel.Information
        //                                 && x.Message.Contains("The Crawler is ready to use.",
        //                                 System.StringComparison.InvariantCultureIgnoreCase))
        //                       .Should().BeFalse();
        //    }
        //}
    }
}
