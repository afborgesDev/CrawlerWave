using CrabsWave.Core;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerNavigationTest
    {
        private const string urlBase = "https://www.google.com.br";

        [Fact]
        public void ShouldNavigateToUrl()
        {
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
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
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl($"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}", out _)
                   .GetElementById("inputName", out var element);

                element.SendKeys("someInput");

                sut.RefreshPage()
                   .GetElementById("inputName", out element);

                element.Text.Should().BeNullOrEmpty();
            }
        }


        [Fact]
        public void ShouldNavigateBack()
        {
            var logmoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logmoq.Object))
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
    }
}
