using System;
using CrabsWave.Core.Resources;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Support.UI;

namespace CrabsWave.Core.Functionalities
{
    public class SelectManager
    {
        public const string LoggerCategory = "CrawlerWave.SelectManager";
        private readonly ILogger Logger;

        public SelectManager(ILogger logger) => Logger = logger;

        public void SelectByText(Crawler parent, string identify, ElementsType elementsType, string textToSelect, bool shouldRetry)
        {
            var element = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                              .TryGetElement(parent, identify, elementsType, shouldRetry);

            if (element == null)
            {
                Logger.LogError($"Could not find a select with the identify: {identify}");
                return;
            }

            var selectObject = new SelectElement(element);
            try
            {
                selectObject.SelectByText(textToSelect);
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not select element: {identify} by using the text: {textToSelect}", e);
            }
        }
    }
}
