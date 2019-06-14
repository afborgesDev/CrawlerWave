using System;
using CrabsWave.Core.LogsReports;
using CrabsWave.Core.Resources;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities
{
    internal static class ClickManager
    {
        public static void Click(IWebDriver driver, string identify, ElementsType elementsType)
        {
            var element = ElementsManager.TryGetElement(driver, identify, elementsType);
            if (element == null) return;
            try
            {
                element.Click();
            }
            catch (Exception e)
            {
                LogManager.LogError($"Could not click at the element: {identify}. ", e);
            }
        }

        public static void ClickFirst(IWebDriver driver, string identify, ElementsType elementsType)
        {
            var elements = ElementsManager.TryGetElements(driver, identify, elementsType);
            if (elements == null || elements.Count <= 0) return;
            try
            {
                elements[0].Click();
            }
            catch (Exception e)
            {
                LogManager.LogError($"Could not click at the first element: {identify}. ", e);
            }
        }
    }
}
