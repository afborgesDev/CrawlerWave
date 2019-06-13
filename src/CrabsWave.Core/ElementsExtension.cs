using System.Collections.ObjectModel;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;
using OpenQA.Selenium;

namespace CrabsWave.Core
{
    public static class ElementsExtension
    {
        public static Crawler GetElementByCssSelector(this Crawler parent, string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(parent.Driver, identify, ElementsType.ClassName, true);
            return parent;
        }

        public static Crawler GetElementById(this Crawler parent, string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(parent.Driver, identify, ElementsType.Id, true);
            return parent;
        }

        public static Crawler GetElementByLinkText(this Crawler parent, string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(parent.Driver, identify, ElementsType.LinkText, true);
            return parent;
        }

        public static Crawler GetElementByName(this Crawler parent, string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(parent.Driver, identify, ElementsType.Name, true);
            return parent;
        }

        public static Crawler GetElementByPartialText(this Crawler parent, string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(parent.Driver, identify, ElementsType.PartialLinkText, true);
            return parent;
        }

        public static Crawler GetElementByTagName(this Crawler parent, string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(parent.Driver, identify, ElementsType.TagName, true);
            return parent;
        }

        public static Crawler GetElementByXPath(this Crawler parent, string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(parent.Driver, identify, ElementsType.XPath, true);
            return parent;
        }

        public static Crawler GetElementsByCssSelector(this Crawler parent, string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(parent.Driver, identify, ElementsType.CssSelector, true);
            return parent;
        }

        public static Crawler GetElementsById(this Crawler parent, string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(parent.Driver, identify, ElementsType.Id, true);
            return parent;
        }

        public static Crawler GetElementsByLinkText(this Crawler parent, string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(parent.Driver, identify, ElementsType.LinkText, true);
            return parent;
        }

        public static Crawler GetElementsByName(this Crawler parent, string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(parent.Driver, identify, ElementsType.Name, true);
            return parent;
        }

        public static Crawler GetElementsByPartialText(this Crawler parent, string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(parent.Driver, identify, ElementsType.PartialLinkText, true);
            return parent;
        }

        public static Crawler GetElementsByTagName(this Crawler parent, string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(parent.Driver, identify, ElementsType.TagName, true);
            return parent;
        }

        public static Crawler GetElementsByXPath(this Crawler parent, string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(parent.Driver, identify, ElementsType.XPath, true);
            return parent;
        }
    }
}
