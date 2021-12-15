using CrawlerWave.Core.Resources;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Interactions;

namespace CrawlerWave.Core.Functionalities;

internal class MouseManager : BaseManager
{
    private const string LoggerCategoryName = "CrawlerWave.MouseManager";

    public MouseManager(ILogger logger) : base(LoggerCategoryName, logger)
    {
    }

    public static MouseManager New(Crawler parent) => new(parent.CreateLogger(LoggerCategoryName));

    public void MoveTo(Crawler parent, WebElementType webElementType, bool shouldClick)
    {
        var element = ElementsManager.New(parent).TryGetElement(parent, webElementType);
        if (element is null) return;

        var action = new Actions(parent.Driver);
        action = action.MoveToElement(element);

        if (shouldClick)
            action.Click();

        action.Build();
        action.Perform();
    }
}
