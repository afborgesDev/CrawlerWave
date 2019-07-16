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
            webElement = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                             .TryGetElement(parent, identify, elementsType, shouldRetry);
            return parent;
        }

        public static Crawler GetElements(this Crawler parent, string identify, ElementsType elementsType, bool shouldRetry, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                         .TryGetElements(parent, identify, elementsType, shouldRetry);
            return parent;
        }

        public static Crawler GetElementAttribute(this Crawler parent, string identify, ElementsType elementsType, string attribute, bool shouldRetry, out string attributeValue)
        {
            attributeValue = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                                .TryGetAttribute(parent, identify, elementsType, attribute, shouldRetry);
            return parent;
        }
    }
}
