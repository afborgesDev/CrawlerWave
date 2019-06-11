using System;
using CrabsWave.Core.Functionalities.Base;
using CrabsWave.Core.resources;

namespace CrabsWave.Core.Functionalities.Interactions
{
    public class CrawlerClick : BaseForFunctionalityClasses, ICrawlerClick
    {
        public CrawlerClick(ICrawler crawler, OpenQA.Selenium.IWebDriver driver) : base(crawler, driver)
        {
        }

        public ICrawler ByClassName(string identify)
        {
            ClickManager.Click(driver, identify, ElementsType.ClassName);
            return crawler;
        }

        public ICrawler ByClassNameIfTrue(string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(driver, identify, ElementsType.ClassName);            
            return crawler;
        }

        public ICrawler ByClassNameUsingScript(string identify) => throw new NotImplementedException();

        public ICrawler ByCssSelector(string identify)
        {
            ClickManager.Click(driver, identify, ElementsType.CssSelector);
            return crawler;
        }

        public ICrawler ByCssSelectorIfTrue(string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(driver, identify, ElementsType.CssSelector);
            return crawler;
        }

        public ICrawler ByCssSelectorUsingScript(string identify) => throw new NotImplementedException();

        public ICrawler ById(string identify)
        {
            ClickManager.Click(driver, identify, ElementsType.Id);
            return crawler;
        }

        public ICrawler ByIdIfTrue(string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(driver, identify, ElementsType.Id);
            return crawler;
        }

        public ICrawler ByIdUsingScript(string identify) => throw new NotImplementedException();

        public ICrawler ByLinkText(string identify)
        {
            ClickManager.Click(driver, identify, ElementsType.LinkText);
            return crawler;
        }

        public ICrawler ByLinkTextIfTrue(string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(driver, identify, ElementsType.LinkText);
            return crawler;
        }

        public ICrawler ByLinkTextUsingScript(string identify) => throw new NotImplementedException();

        public ICrawler ByName(string identify)
        {
            ClickManager.Click(driver, identify, ElementsType.Name);
            return crawler;
        }

        public ICrawler ByNameIfTrue(string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(driver, identify, ElementsType.Name);
            return crawler;
        }

        public ICrawler ByNameUsingScript(string identify) => throw new NotImplementedException();

        public ICrawler ByPartialLinkText(string identify)
        {
            ClickManager.Click(driver, identify, ElementsType.PartialLinkText);
            return crawler;
        }

        public ICrawler ByPartialLinkTextIfTrue(string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(driver, identify, ElementsType.PartialLinkText);
            return crawler;
        }

        public ICrawler ByPartialLinkTextUsingScript(string identify) => throw new NotImplementedException();

        public ICrawler ByTagName(string identify)
        {
            ClickManager.Click(driver, identify, ElementsType.TagName);
            return crawler;
        }

        public ICrawler ByTagNameIfTrue(string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(driver, identify, ElementsType.TagName);
            return crawler;
        }

        public ICrawler ByTagNameUsingScript(string identify) => throw new NotImplementedException();

        public ICrawler ByXPath(string identify)
        {
            ClickManager.Click(driver, identify, ElementsType.XPath);
            return crawler;
        }

        public ICrawler ByXPathIfTrue(string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(driver, identify, ElementsType.XPath);
            return crawler;
        }

        public ICrawler ByXPathUsingScript(string identify) => throw new NotImplementedException();

        public ICrawler ClickAlert(bool acept)
        {
            if (acept)
                driver.SwitchTo().Alert().Accept();
            else
                driver.SwitchTo().Alert().Dismiss();

            return crawler;
        }

        public ICrawler FirstByClassName(string identify)
        {
            ClickManager.ClickFirst(driver, identify, ElementsType.ClassName);
            return crawler;
        }

        public ICrawler FirstByCssSelector(string identify)
        {
            ClickManager.ClickFirst(driver, identify, ElementsType.CssSelector);
            return crawler;
        }

        public ICrawler FirstById(string identify)
        {
            ClickManager.ClickFirst(driver, identify, ElementsType.Id);
            return crawler;
        }

        public ICrawler FirstByLinkText(string identify)
        {
            ClickManager.ClickFirst(driver, identify, ElementsType.LinkText);
            return crawler;
        }

        public ICrawler FirstByName(string identify)
        {
            ClickManager.ClickFirst(driver, identify, ElementsType.Name);
            return crawler;
        }

        public ICrawler FirstByPartialLinkText(string identify)
        {
            ClickManager.ClickFirst(driver, identify, ElementsType.PartialLinkText);
            return crawler;
        }

        public ICrawler FirstByTagName(string identify)
        {
            ClickManager.ClickFirst(driver, identify, ElementsType.TagName);
            return crawler;
        }

        public ICrawler FirstByXPath(string identify)
        {
            ClickManager.ClickFirst(driver, identify, ElementsType.XPath);
            return crawler;
        }
    }
}
