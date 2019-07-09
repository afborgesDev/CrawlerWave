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
                return ((IJavaScriptExecutor)driver).ExecuteScript(script).ToString();
            }
            catch (Exception e)
            {
                LogManager.Instance.LogError("Could not execute javascript and take a result", e);
                return string.Empty;
            }
        }

        public static void ExecuteScriptUsingJavaScriptExecutor(IWebDriver driver, string script, params object[] args)
        {
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript(script, args);
            }
            catch (Exception e)
            {
                LogManager.Instance.LogError("Could not execute javascript using args and JavaScriptExecutor engine", e);
            }
        }
    }
}
