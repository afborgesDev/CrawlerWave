using System;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities.Scripts
{
    internal class ScriptManager
    {
        public const string LoggerCategory = "CrawlerWave.ScriptManager";
        private readonly ILogger Logger;

        public ScriptManager(ILogger logger) => Logger = logger;

        public string ExecuteAndTakeResult(Crawler parent, string script)
        {
            try
            {
                return ((IJavaScriptExecutor)parent.Driver).ExecuteScript(script).ToString();
            }
            catch (Exception e)
            {
                Logger.LogError("Could not execute javascript and take a result", e);
                return string.Empty;
            }
        }

        public void ExecuteScriptUsingJavaScriptExecutor(Crawler parent, string script, params object[] args)
        {
            try
            {
                ((IJavaScriptExecutor)parent.Driver).ExecuteScript(script, args);
            }
            catch (Exception e)
            {
                Logger.LogError("Could not execute javascript using args and JavaScriptExecutor engine", e);
            }
        }
    }
}
