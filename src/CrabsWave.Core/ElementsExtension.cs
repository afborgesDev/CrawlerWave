using System.Collections.ObjectModel;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;
using OpenQA.Selenium;

namespace CrabsWave.Core
{
    public static class ElementsExtension
    {
        public static Crawler GetElement(this Crawler parent, WebElementType webElementType, out IWebElement webElement)
        {
            webElement = ElementsManager.New(parent).TryGetElement(parent, webElementType);
            return parent;
        }

        public static Crawler GetElements(this Crawler parent, WebElementType webElementType, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.New(parent).TryGetElements(parent, webElementType);
            return parent;
        }

        public static Crawler GetElementAttribute(this Crawler parent, WebElementType webElementType, string attribute, out string attributeValue)
        {
            attributeValue = ElementsManager.New(parent).TryGetAttribute(parent, webElementType, attribute);
            return parent;
        }

        public static Crawler RemoveElement(this Crawler parent, WebElementType webElementType)
        {
            ElementsManager.New(parent).RemoveElement(parent, webElementType);
            return parent;
        }
    }
}
