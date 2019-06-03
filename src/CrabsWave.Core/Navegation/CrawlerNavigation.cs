using System;
using CrabsWave.Core.Base;
using OpenQA.Selenium;

namespace CrabsWave.Core.Navegation
{
    public class CrawlerNavigation : BaseForFunctionalityClasses, ICrawlerNavigation
    {
        public CrawlerNavigation(ICrawler crawler, IWebDriver driver) : base(crawler, driver)
        {
        }

        public ICrawler GoToUrl(string url, out string errorMessage)
        {
            throw new NotImplementedException();
        }

    }
}
