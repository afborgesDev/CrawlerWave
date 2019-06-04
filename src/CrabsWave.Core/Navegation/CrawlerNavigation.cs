using System;
using System.Drawing;
using System.Threading;
using CrabsWave.Core.Base;
using OpenQA.Selenium;

namespace CrabsWave.Core.Navegation
{
    public class CrawlerNavigation : BaseForFunctionalityClasses, ICrawlerNavigation
    {
        public CrawlerNavigation(ICrawler crawler, IWebDriver driver) : base(crawler, driver)
        {
        }

        public ICrawler GoToUrl(string url, out string errorMessage)
        {
            const int MaxAttemptsAtFive = 5;
            var TwoSecondsToWait = TimeSpan.FromSeconds(2);
            errorMessage = string.Empty;

            if (driver == null)
            {
                errorMessage = "Could not navigate, the driver was not well initializated.";
                return crawler;
            }

            for (var i = 1; i < MaxAttemptsAtFive; i++)
            {
                var (ok, message) = DoNavigateToUrl(url);
                if (ok)
                {
                    driver.Manage().Window.Maximize();
                    return crawler;
                }

                if (string.IsNullOrEmpty(message))
                {
                    SetDefaultWindowSize();
                    return crawler;
                }

                errorMessage = message;
                Thread.Sleep(TwoSecondsToWait);
            }

            return crawler;
        }

        public ICrawler NavigateBack() => Execute(() => driver.Navigate().Back());

        public ICrawler GetCurrentUrl(out string url)
        {
            url = driver.Url;
            return crawler;
        }

        public ICrawler RefreshPage() => Execute(() => driver.Navigate().Refresh());

        public ICrawler SwitchToWindow(string windowName) => Execute(() => SwitchTo(windowName));

        public ICrawler SwitchToFrame(string frameName) => throw new NotImplementedException();

        public ICrawler SwitchToFrame(IWebElement webElement)
        {
            if (webElement == null) return crawler;
            try
            {
                driver.SwitchTo().Frame(webElement);
            }
            catch (Exception e)
            {
                //Should Log this
            }
            return crawler;
        }

        private (bool, string) DoNavigateToUrl(string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.SwitchTo().Window(driver.CurrentWindowHandle);
                return (driver.Url.Equals(url), string.Empty);
            }
            catch (WebDriverTimeoutException e) { return (false, $"Could not navigate to url. TimeOut {e.Message}"); }
            catch (WebDriverException e) { return (false, $"Could not navigate to url. Internal error {e.Message}"); }
            catch (Exception e) { return (false, $"Could not navigate to url. Unkonwn error {e.Message}"); }
        }

        private void SetDefaultWindowSize() => driver.Manage().Window.Size = new Size(1680, 1050);

        private void SwitchTo(string windowsName)
        {
            if (!string.IsNullOrWhiteSpace(windowsName))
                driver.SwitchTo().Window(windowsName);
        }
    }
}
