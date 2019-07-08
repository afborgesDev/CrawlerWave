using System;
using System.Collections.Generic;
using CrabsWave.Core.LogsReports;
using CrabsWave.Core.Resources;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities
{
    internal static class TextManager
    {
        private const string AttributeText = "innerText";

        public static string GetElementInnerText(IWebDriver driver, string identify, ElementsType elementsType) => ElementsManager.TryGetAttribute(driver, identify, elementsType, AttributeText);

        public static IList<string> GetTextFromMultipleElementOcurrences(IWebDriver driver, string identify, ElementsType elementsType)
        {
            var items = ElementsManager.TryGetElements(driver, identify, elementsType);
            if (items == null || items.Count <= 0) return default;

            var returnList = new List<string>(items.Count);
            foreach (var item in items)
                returnList.Add(item.Text);

            return returnList;
        }

        public static void ClearAndSendKeys(IWebDriver driver, string identify, ElementsType elementsType, string textToSend)
        {
            var item = ElementsManager.TryGetElement(driver, identify, elementsType);
            try
            {
                item.Clear();
                item.SendKeys(textToSend);
            }
            catch (Exception e)
            {
                LogManager.Instance.LogError($"Cuold not clear and send key for element {identify}", e);
            }
        }
    }
}
