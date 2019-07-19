using System.IO;
using CrabsWave.Core;
using CrabsWave.Utils.IO;
using CrawlerWave.LogTestUtils;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerScreenShotTest
    {
        [Fact]
        public void ShouldTakeScreenShotAsStream()
        {
            var (_, logger) = CreateForTest.Create();
            using (var sut = new Crawler(logger))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .ScreenShotToStream(out var imageStream);

                imageStream.Should().NotBeNull();
            }
        }

        [Fact]
        public void ShouldTakeScreenShotAsBase64()
        {
            var (_, logger) = CreateForTest.Create();
            using (var sut = new Crawler(logger))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .ScreenShotToBase64(out var imageStream);

                imageStream.Should().NotBeNull();
            }
        }

        [Fact]
        public void ShouldTakeScreenShotFromFile()
        {
            const string fileName = "myTestFileName.png";
            var (_, logger) = CreateForTest.Create();
            using (var sut = new Crawler(logger))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(PageForUnitTestHelper.GetUrlForUniTestFile(), out _)
                   .ScreenShotToFile(SuportedImageTypes.PNG, fileName);

                var fileCreated = File.Exists(fileName);
                fileCreated.Should().BeTrue();
                File.Delete(fileName);
            }
        }
    }
}
