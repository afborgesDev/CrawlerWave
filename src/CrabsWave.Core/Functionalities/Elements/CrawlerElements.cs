using System;
using System.Collections.Generic;
using System.Text;
using CrabsWave.Core.Functionalities.Base;
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
            throw new NotImplementedException();
        }

        public ICrawler GetElementById(string identify, out IWebElement webElement)
        {
            throw new NotImplementedException();
        }

        public ICrawler GetElementByLinkText(string identify, out IWebElement webElement)
        {
            throw new NotImplementedException();
        }

        public ICrawler GetElementByName(string identify, out IWebElement webElement)
        {
            throw new NotImplementedException();
        }

        public ICrawler GetElementByPartialText(string identify, out IWebElement webElement)
        {
            throw new NotImplementedException();
        }

        public ICrawler GetElementByTagName(string identify, out IWebElement webElement)
        {
            throw new NotImplementedException();
        }

        public ICrawler GetElementByXPath(string identify, out IWebElement webElement)
        {
            throw new NotImplementedException();
        }
    }
}
