using CrabsWave.Core;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerNavigationTest
    {
        const string urlBase = "https://www.google.com.br";
        [Fact]
        public void ShouldNavigateToUrl()
        {
            var logmoq = new Mock<ILogger<ICrawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior());
                sut.Navigation()
                   .GoToUrl(urlBase, out var errorMessage)
                   .Navigation()
                   .GetCurrentUrl(out var currentUrl);

                errorMessage.Should().BeNullOrEmpty();
                currentUrl.Should().ContainAll(urlBase);
            }
        }

        [Fact]
        public void ShouldNavigateBack()
        {
            var logmoq = new Mock<ILogger<ICrawler>>();
            using (var sut = new Crawler(logmoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior());
                sut.Navigation()
                   .GoToUrl(urlBase, out var errorMessage)
                   .Navigation()
                   .GetCurrentUrl(out var currentUrl);

                errorMessage.Should().BeNullOrEmpty();
                currentUrl.Should().ContainAll(urlBase);

                sut.Navigation()
                   .GoToUrl("https://github.com", out errorMessage)
                   .Navigation()
                   .GetCurrentUrl(out currentUrl);

                errorMessage.Should().BeNullOrEmpty();
                currentUrl.Should().ContainAll("https://github.com");

                sut.Navigation()
                   .NavigateBack()
                   .Navigation()
                   .GetCurrentUrl(out currentUrl);

                errorMessage.Should().BeNullOrEmpty();
                currentUrl.Should().ContainAll(urlBase);
            }
        }
    }
}
