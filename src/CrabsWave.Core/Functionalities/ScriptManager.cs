using System;
using CrabsWave.Core.LogsReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace CrabsWave.Core.Functionalities.Scripts
{
    internal static class ScriptManager
    {
        public static void ExecuteScript(IWebDriver driver, string script)
        {
            if (driver is ChromeDriver cdriver)
                cdriver.ExecuteScript(script);

            if (driver is RemoteWebDriver rdriver)
                rdriver.ExecuteScript(script);
        }

        public static void ExecuteScript(IWebDriver driver, string script, params object[] args) => ExecuteScript(driver, string.Format(script, args));

        public static string ExecuteAndTakeResult(IWebDriver driver, string script)
        {
            try
            {
                return (string)((IJavaScriptExecutor)driver).ExecuteScript(script);
            }
            catch (Exception e)
            {
                LogManager.LogError("Could not execute javascript and take a result", e);
                return string.Empty;
            }
        }

        public static void ExecuteScriptUsingJavaScriptExecutor(IWebDriver driver, string script, params object[] args)
        {
            if (string.IsNullOrEmpty(script)) return;

            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript(script, args);
            }
            catch (Exception e)
            {
                LogManager.LogError("Could not execute javascript using args and JavaScriptExecutor engine", e);
            }
        }
    }
}
