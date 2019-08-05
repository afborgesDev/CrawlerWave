using System.IO;
using CrawlerWave.Core;
using CrawlerWave.Core.Configurations;
using CrawlerWave.LogTestHelper;
using CrawlerWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrawlerWave.Test.Core.CrawlerTests
{
    public class CrawlerScreenShotTest
    {
        [Fact]
        public void ShouldTakeScreenShotAsStream()
        {
            var (_, logger) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(logger))
            {
                sut.Initializate(new Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .ScreenShotToStream(out var imageStream);

                imageStream.Should().NotBeNull();
            }
        }

        [Fact]
        public void ShouldTakeScreenShotAsBase64()
        {
            var (_, logger) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(logger))
            {
                sut.Initializate(new Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .ScreenShotToBase64(out var imageStream);

                imageStream.Should().NotBeNull();
            }
        }

        [Fact]
        public void ShouldTakeScreenShotFromFile()
        {
            const string fileName = "myTestFileName.png";
            var (_, logger) = LogTestHelperInitialization.Create();
            using (var sut = new Crawler(logger))
            {
                sut.Initializate(new Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .ScreenShotToFile(SuportedImageTypes.PNG, fileName);

                var fileCreated = File.Exists(fileName);
                fileCreated.Should().BeTrue();
                File.Delete(fileName);
            }
        }
    }
}
