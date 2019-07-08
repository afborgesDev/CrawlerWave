using System;
using CrabsWave.Core.LogsReports;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities.Scripts
{
    internal static class ScriptManager
    {
        public static string ExecuteAndTakeResult(IWebDriver driver, string script)
        {
            try
            {
                LogManager.Instance.LogInformation("Starting to run the script");
                return ((IJavaScriptExecutor)driver).ExecuteScript(script).ToString();
            }
            catch (Exception e)
            {
                LogManager.Instance.LogInformation("Informatio of error Starting to run the script");
                LogManager.Instance.LogError("Could not execute javascript and take a result", e);
                return string.Empty;
            }
        }

        public static void ExecuteScriptUsingJavaScriptExecutor(IWebDriver driver, string script, params object[] args)
        {
            try
            {
                LogManager.Instance.LogInformation("Starting to run the script");
                ((IJavaScriptExecutor)driver).ExecuteScript(script, args);
            }
            catch (Exception e)
            {
                LogManager.Instance.LogInformation("Informatio of error Starting to run the script");
                LogManager.Instance.LogError("Could not execute javascript using args and JavaScriptExecutor engine", e);
            }
        }
    }
}
