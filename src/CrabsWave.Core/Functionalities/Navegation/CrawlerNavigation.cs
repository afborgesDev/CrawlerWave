using System;
using System.Drawing;
using System.Threading;
using CrabsWave.Core.Functionalities.Base;
using CrabsWave.Core.LogsReports;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities.Navegation
{
    public class CrawlerNavigation : BaseForFunctionalityClasses, ICrawlerNavigation
    {
        public CrawlerNavigation(Crawler crawler, IWebDriver driver) : base(crawler, driver)
        {
        }

        public Crawler GoToUrl(string url, out string errorMessage)
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

        public Crawler NavigateBack() => Execute(() => driver.Navigate().Back());

        public Crawler GetCurrentUrl(out string url)
        {
            url = driver.Url;
            return crawler;
        }

        public Crawler RefreshPage() => Execute(() => driver.Navigate().Refresh());

        public Crawler SwitchToWindow(string windowName) => Execute(() => SwitchTo(windowName));

        public Crawler SwitchToFrame(string frameName) => throw new NotImplementedException();

        public Crawler SwitchToFrame(IWebElement webElement)
        {
            if (webElement == null) return crawler;
            try
            {
                driver.SwitchTo().Frame(webElement);
            }
            catch (Exception e)
            {
                LogManager.LogError("Could not swith to frame", e);
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
            catch (WebDriverTimeoutException e)
            {
                var message = $"Could not navigate to url. TimeOut {e.Message}";
                LogManager.LogError(message);
                return (false, message);
            }
            catch (WebDriverException e)
            {
                var message = $"Could not navigate to url. Internal error {e.Message}";
                LogManager.LogError(message);
                return (false, message);
            }
            catch (Exception e)
            {
                var message = $"Could not navigate to url. Unkonwn error {e.Message}";
                LogManager.LogError(message);
                return (false, message);
            }
        }

        private void SetDefaultWindowSize() => driver.Manage().Window.Size = new Size(1680, 1050);

        private void SwitchTo(string windowsName)
        {
            if (!string.IsNullOrWhiteSpace(windowsName))
                driver.SwitchTo().Window(windowsName);
        }
    }
}
