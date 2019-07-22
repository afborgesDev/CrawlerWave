using System;
using CrabsWave.Core.Resources;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Support.UI;

namespace CrabsWave.Core.Functionalities
{
    internal class SelectManager : BaseManager
    {
        public SelectManager(ILogger logger) : base("CrawlerWave.SelectManager", logger)
        {
        }

        public void SelectByText(Crawler parent, WebElementType webElementType, string textToSelect, bool shouldRetry)
        {
            var element = ElementsManager.New(parent).TryGetElement(parent, webElementType, shouldRetry);

            if (element == null)
            {
                Logger.LogError($"Could not find a select with the identify: {webElementType.Identify}");
                return;
            }

            var selectObject = new SelectElement(element);
            try
            {
                selectObject.SelectByText(textToSelect);
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not select element: {webElementType.Identify} by using the text: {textToSelect}", e);
            }
        }

        public void SelectByValue(Crawler parent, WebElementType webElementType, string valueToSelect, bool shouldRetry)
        {
            var element = ElementsManager.New(parent).TryGetElement(parent, webElementType, shouldRetry);

            if (element == null)
            {
                Logger.LogError($"Could not find a select with the identify: {webElementType.Identify}");
                return;
            }

            var selectObject = new SelectElement(element);
            try
            {
                selectObject.SelectByValue(valueToSelect);
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not select element: {webElementType.Identify} by using the value: {valueToSelect}", e);
            }
        }

        public void SelectByIndex(Crawler parent, WebElementType webElementType, int indexToSelect, bool shouldRetry)
        {
            var element = ElementsManager.New(parent).TryGetElement(parent, webElementType, shouldRetry);

            if (element == null)
            {
                Logger.LogError($"Could not find a select with the identify: {webElementType.Identify}");
                return;
            }

            var selectObject = new SelectElement(element);
            try
            {
                selectObject.SelectByIndex(indexToSelect);
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not select element: {webElementType.Identify} by using the index: {indexToSelect}", e);
            }
        }
    }
}
