using CrabsWave.Core.Resources;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Interactions;

namespace CrabsWave.Core.Functionalities
{
    internal class MouseManager : BaseManager
    {
        public MouseManager(ILogger logger) : base("CrawlerWave.MouseManager", logger)
        {
        }

        public static MouseManager New(Crawler parent) => new MouseManager(parent.CreateLogger(LoggerCategory));

        public void MoveTo(Crawler parent, WebElementType webElementType, bool shouldClick)
        {
            var element = ElementsManager.New(parent).TryGetElement(parent, webElementType);
            if (element == null) return;

            var action = new Actions(parent.Driver);
            action = action.MoveToElement(element);

            if (shouldClick)
                action.Click();

            action.Build();
            action.Perform();
        }
    }
}
