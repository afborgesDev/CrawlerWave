using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities.Navegation
{
    public interface ICrawlerNavigation
    {
        Crawler GoToUrl(string url, out string errorMessage);
        Crawler NavigateBack();
        Crawler GetCurrentUrl(out string url);
        Crawler RefreshPage();
        Crawler SwitchToWindow(string windowName);
        Crawler SwitchToFrame(string frameName);
        Crawler SwitchToFrame(IWebElement webElement);
    }
}
