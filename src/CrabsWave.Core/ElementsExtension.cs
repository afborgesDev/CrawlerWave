using System.Collections.ObjectModel;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;
using OpenQA.Selenium;

namespace CrabsWave.Core
{
    public static class ElementsExtension
    {
        public static Crawler GetElement(this Crawler parent, string identify, ElementsType elementsType, out IWebElement webElement)
        {
            parent.RestoreLog();
            webElement = ElementsManager.TryGetElement(parent.Driver, identify, elementsType, true);
            return parent;
        }

        public static Crawler GetElements(this Crawler parent, string identify, ElementsType elementsType, out ReadOnlyCollection<IWebElement> webElement)
        {
            parent.RestoreLog();
            webElement = ElementsManager.TryGetElements(parent.Driver, identify, elementsType, true);
            return parent;
        }

        public static Crawler GetElementAttribute(this Crawler parent, string identify, ElementsType elementsType, string attribute, out string attributeValue)
        {
            parent.RestoreLog();
            attributeValue = ElementsManager.TryGetAttribute(parent.Driver, identify, elementsType, attribute);
            return parent;
        }
    }
}
