using System.Collections.ObjectModel;
using CrabsWave.Core.Functionalities.Base;
using CrabsWave.Core.resources;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities.Elements
{
    public class CrawlerElements : BaseForFunctionalityClasses, ICrawlerElements
    {
        public CrawlerElements(ICrawler crawler, IWebDriver driver) : base(crawler, driver)
        {
        }

        public ICrawler GetElementByCssSelector(string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(driver, identify, ElementsType.ClassName, true);
            return crawler;
        }

        public ICrawler GetElementById(string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(driver, identify, ElementsType.Id, true);
            return crawler;
        }

        public ICrawler GetElementByLinkText(string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(driver, identify, ElementsType.LinkText, true);
            return crawler;
        }

        public ICrawler GetElementByName(string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(driver, identify, ElementsType.Name, true);
            return crawler;
        }

        public ICrawler GetElementByPartialText(string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(driver, identify, ElementsType.PartialLinkText, true);
            return crawler;
        }

        public ICrawler GetElementByTagName(string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(driver, identify, ElementsType.TagName, true);
            return crawler;
        }

        public ICrawler GetElementByXPath(string identify, out IWebElement webElement)
        {
            webElement = ElementsManager.TryGetElement(driver, identify, ElementsType.XPath, true);
            return crawler;
        }

        public ICrawler GetElementsByCssSelector(string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(driver, identify, ElementsType.CssSelector, true);
            return crawler;
        }

        public ICrawler GetElementsById(string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(driver, identify, ElementsType.Id, true);
            return crawler;
        }

        public ICrawler GetElementsByLinkText(string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(driver, identify, ElementsType.LinkText, true);
            return crawler;
        }

        public ICrawler GetElementsByName(string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(driver, identify, ElementsType.Name, true);
            return crawler;
        }

        public ICrawler GetElementsByPartialText(string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(driver, identify, ElementsType.PartialLinkText, true);
            return crawler;
        }

        public ICrawler GetElementsByTagName(string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(driver, identify, ElementsType.TagName, true);
            return crawler;
        }

        public ICrawler GetElementsByXPath(string identify, out ReadOnlyCollection<IWebElement> webElement)
        {
            webElement = ElementsManager.TryGetElements(driver, identify, ElementsType.XPath, true);
            return crawler;
        }
    }
}
