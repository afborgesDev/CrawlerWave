﻿using System.Drawing;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace CrawlerWave.Core.Functionalities;

//TODO: Should revisit this class seems it can be simplified
internal class NavegationManager : BaseManager
{
    private const string LoggerCategoryName = "CrawlerWave.NavegationManager";

    public NavegationManager(ILogger logger) : base(LoggerCategoryName, logger)
    {
    }

    public static NavegationManager New(Crawler parent) => new(parent.CreateLogger(LoggerCategoryName));

    public string GetCurrentUrl(Crawler parent) => parent.Driver.Url;

    public void RefreshPage(Crawler parent) => parent.Driver.Navigate().Refresh();

    public void NavigateBack(Crawler parent) => parent.Driver.Navigate().Back();

    public string GoToUrl(Crawler parent, string url)
    {
        const int MaxAttemptsAtFive = 5;
        var TwoSecondsToWait = TimeSpan.FromSeconds(2);

        if (parent.Driver == null)
            return "Could not navigate, the driver was not well initializated.";

        bool ok;
        var message = string.Empty;

        for (var i = 1; i < MaxAttemptsAtFive; i++)
        {
            (ok, message) = DoNavigateToUrl(parent, url);
            if (ok)
            {
                parent.Driver.Manage().Window.Maximize();
                return string.Empty;
            }

            if (string.IsNullOrEmpty(message))
            {
                SetDefaultWindowSize(parent);
                return string.Empty;
            }

            Thread.Sleep(TwoSecondsToWait);
        }

        return message;
    }

    private static void SetDefaultWindowSize(Crawler parent) => parent.Driver.Manage().Window.Size = new Size(1680, 1050);

    private (bool, string) DoNavigateToUrl(Crawler? parent, string url)
    {
        if (parent is null || parent.Driver is null)
            throw new ArgumentNullException(nameof(parent));
        try
        {
            parent.Driver.Navigate().GoToUrl(url);
            parent.Driver.SwitchTo().Window(parent.Driver.CurrentWindowHandle);
            return (parent.Driver.Url.Equals(url), string.Empty);
        }
        catch (Exception e)
        {
            var message = $"Could not navigate to url. Unkonwn error {e.Message}";
            Logger.LogError(message, e);
            return (false, message);
        }
    }
}
