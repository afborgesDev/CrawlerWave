using System;
using System.Drawing;
using System.Threading;
using CrabsWave.Core.LogsReports;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities
{
    public static class NavegationManager
    {
        public static string GoToUrl(IWebDriver driver, string url)
        {
            const int MaxAttemptsAtFive = 5;
            var TwoSecondsToWait = TimeSpan.FromSeconds(2);

            if (driver == null)
            {
                return "Could not navigate, the driver was not well initializated.";
            }

            bool ok;
            var message = string.Empty;

            for (var i = 1; i < MaxAttemptsAtFive; i++)
            {
                (ok, message) = DoNavigateToUrl(driver, url);
                if (ok)
                {
                    driver.Manage().Window.Maximize();
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(message))
                {
                    SetDefaultWindowSize(driver);
                    return string.Empty;
                }

                Thread.Sleep(TwoSecondsToWait);
            }

            return message;
        }

        public static void NavigateBack(IWebDriver driver) => driver.Navigate().Back();

        public static string GetCurrentUrl(IWebDriver driver) => driver.Url;

        public static void RefreshPage(IWebDriver driver) => driver.Navigate().Refresh();

        private static (bool, string) DoNavigateToUrl(IWebDriver driver, string url)
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

        private static void SetDefaultWindowSize(IWebDriver driver) => driver.Manage().Window.Size = new Size(1680, 1050);
    }
}
