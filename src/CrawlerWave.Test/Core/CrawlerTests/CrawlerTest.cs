using CrawlerWave.Core;
using CrawlerWave.Core.Configurations;
using CrawlerWave.LogTestHelper;
using FluentAssertions;
using Xunit;

namespace CrawlerWave.Test.Core.CrawlerTests
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
    }
}
