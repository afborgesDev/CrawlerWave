using System;
using CrabsWave.Core.LogsReports;
using CrabsWave.Core.Resources;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CrabsWave.Core.Functionalities
{
    public static class SelectManager
    {
        public static void SelectByText(IWebDriver driver, string identify, ElementsType elementsType, string textToSelect, bool shouldRetry)
        {
            var element = ElementsManager.TryGetElement(driver, identify, elementsType, shouldRetry);

            if (element == null)
            {
                LogManager.Instance.LogError($"Could not find a select with the identify: {identify}");
                return;
            }

            var selectObject = new SelectElement(element);
            try
            {
                selectObject.SelectByText(textToSelect);
            }
            catch (Exception e)
            {
                LogManager.Instance.LogError($"Could not select element: {identify} by using the text: {textToSelect}", e);
            }
        }
    }
}
