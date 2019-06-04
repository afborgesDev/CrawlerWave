using OpenQA.Selenium;

namespace CrabsWave.Core.Navegation
{
    public interface ICrawlerNavigation
    {
        ICrawler GoToUrl(string url, out string errorMessage);
        ICrawler NavigateBack();
        ICrawler GetCurrentUrl(out string url);
        ICrawler RefreshPage();
        ICrawler SwitchToWindow(string windowName);
        ICrawler SwitchToFrame(string frameName);
        ICrawler SwitchToFrame(IWebElement webElement);
    }
}
