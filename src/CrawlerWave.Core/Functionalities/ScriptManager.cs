﻿using System;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace CrawlerWave.Core.Functionalities
{
    internal class ScriptManager : BaseManager
    {
        public ScriptManager(ILogger logger) : base("CrawlerWave.ScriptManager", logger)
        {
        }

        public static ScriptManager New(Crawler parent) => new(parent.CreateLogger(LoggerCategory));

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
