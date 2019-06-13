using CrabsWave.Core;
using CrabsWave.Core.Configurations;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerTest
    {
        [Fact]
        public void ShouldInitializateCrawler()
        {
            var logmoq = new Mock<ILogger<ICrawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new Behavior());
                sut.Ready.Should().BeTrue();
                sut.Navigation().Should().NotBeNull();
                sut.Elements().Should().NotBeNull();
                sut.Click().Should().NotBeNull();
                sut.Scripts().Should().NotBeNull();
            }
        }
    }
}
