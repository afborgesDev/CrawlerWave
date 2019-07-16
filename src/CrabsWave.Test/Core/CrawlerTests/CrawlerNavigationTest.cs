using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrawlerWave.LogTestUtils;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerNavigationTest
    {
        private const string urlBase = "https://www.google.com.br";

        [Fact]
        public void ShouldNavigateToUrl()
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior());
                sut.GoToUrl(urlBase, out var errorMessage)
                   .GetCurrentUrl(out var currentUrl);

                errorMessage.Should().BeNullOrEmpty();
                currentUrl.Should().ContainAll(urlBase);
            }
        }

        [Fact]
        public void ShouldRefreshPage()
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl($"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}", out _)
                   .GetElement("inputName", ElementsType.Id, true, out var element);

                element.SendKeys("someInput");

                sut.RefreshPage()
                   .GetElement("inputName", ElementsType.Id, true, out element);

                element.Text.Should().BeNullOrEmpty();
            }
        }

        [Fact]
        public void ShouldNavigateBack()
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior());
                sut.GoToUrl(urlBase, out var errorMessage)
                   .GetCurrentUrl(out var currentUrl);

                errorMessage.Should().BeNullOrEmpty();
                currentUrl.Should().ContainAll(urlBase);

                sut.GoToUrl("https://github.com", out errorMessage)
                   .GetCurrentUrl(out currentUrl);

                errorMessage.Should().BeNullOrEmpty();
                currentUrl.Should().ContainAll("https://github.com");

                sut.NavigateBack()
                   .GetCurrentUrl(out currentUrl);

                errorMessage.Should().BeNullOrEmpty();
                currentUrl.Should().ContainAll(urlBase);
            }
        }

        [Fact]
        public void ShouldFailOnNavigate()
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                       .GoToUrl("https:// this is. a wrong. ;\\ url", out var errorMesage);

                errorMesage.Should().Contain("Could not navigate to url. Unkonwn error");
            }
        }

        [Fact]
        public void ShouldFailOnNavigateWithNullDriver()
        {
            var (_, factory) = CreateForTest.Create();
            using (var sut = new Crawler(factory))
            {
                sut.GoToUrl("https:// this is. a wrong. ;\\ url", out var errorMesage);
                errorMesage.Should().Contain("Could not navigate, the driver was not well initializated.");
            }
        }
    }
}
