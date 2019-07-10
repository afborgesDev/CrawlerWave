using System;
using CrabsWave.Core.Functionalities.Scripts;
using CrabsWave.Core.LogsReports;
using CrabsWave.Core.Resources;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities
{
    internal static class ClickManager
    {
        public static void Click(IWebDriver driver, string identify, ElementsType elementsType, bool shouldRetry)
        {
            var element = ElementsManager.TryGetElement(driver, identify, elementsType, shouldRetry);
            element?.Click();
        }

        public static void ClickFirst(IWebDriver driver, string identify, ElementsType elementsType, bool shouldRetry)
        {
            var elements = ElementsManager.TryGetElements(driver, identify, elementsType, shouldRetry);
            try
            {
                elements[0].Click();
            }
            catch (Exception e)
            {
                LogManager.Instance.LogError($"Could not click at the first element: {identify}. ", e);
            }
        }

        public static void ClickUsingJavaScript(IWebDriver driver, string identify, ElementsType elementsType, bool shouldRetry)
        {
            switch (elementsType)
            {
                case ElementsType.Id:
                    ScriptManager.ExecuteScriptUsingJavaScriptExecutor(driver, $"document.getElementById('{identify}').click();");
                    break;

                default:
                {
                    var element = ElementsManager.TryGetElement(driver, identify, elementsType, shouldRetry);
                    ScriptManager.ExecuteScriptUsingJavaScriptExecutor(driver, "(arguments[0] || {click:() => ''}).click();", element);
                    break;
                }
            }
        }
    }
}
