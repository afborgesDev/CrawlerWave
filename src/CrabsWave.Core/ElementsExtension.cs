using System.Collections.ObjectModel;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;
using OpenQA.Selenium;

namespace CrabsWave.Core
{
    public static class ElementsExtension
    {
        public static Crawler GetElement(this Crawler parent, string identify, ElementsType elementsType, bool shouldRetry, out IWebElement webElement)
        {
            parent.RestoreLog();
            webElement = ElementsManager.TryGetElement(parent.Driver, identify, elementsType, shouldRetry);
            return parent;
        }

        public static Crawler GetElements(this Crawler parent, string identify, ElementsType elementsType, bool shouldRetry, out ReadOnlyCollection<IWebElement> webElement)
        {
            parent.RestoreLog();
            webElement = ElementsManager.TryGetElements(parent.Driver, identify, elementsType, shouldRetry);
            return parent;
        }

        public static Crawler GetElementAttribute(this Crawler parent, string identify, ElementsType elementsType, string attribute, bool shouldRetry, out string attributeValue)
        {
            parent.RestoreLog();
            attributeValue = ElementsManager.TryGetAttribute(parent.Driver, identify, elementsType, attribute, shouldRetry);
            return parent;
        }
    }
}
