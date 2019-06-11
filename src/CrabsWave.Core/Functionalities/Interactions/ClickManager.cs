using CrabsWave.Core.Functionalities.Elements;
using CrabsWave.Core.resources;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities.Interactions
{
    internal static class ClickManager
    {
        public static void Click(IWebDriver driver, string identify, ElementsType elementsType)
        {
            var element = ElementsManager.TryGetElement(driver, identify, elementsType);
            if (element == null) return;
            element.Click();
        }

        public static void ClickFirst(IWebDriver driver, string identify, ElementsType elementsType)
        {
            var elements = ElementsManager.TryGetElements(driver, identify, elementsType);
            if (elements == null || elements.Count <= 0) return;
            elements[0].Click();
        }
    }
}
