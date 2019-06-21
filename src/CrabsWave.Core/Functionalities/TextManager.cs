using CrabsWave.Core.Resources;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities
{
    public static class TextManager
    {
        private const string AttributeText = "innerText";
        public static string GetElementText(IWebDriver driver, string identify, ElementsType elementsType) => ElementsManager.TryGetAttribute(driver, identify, elementsType, AttributeText);
    }
}
