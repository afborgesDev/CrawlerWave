using System;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities.Base
{
    public class BaseForFunctionalityClasses
    {
        protected readonly ICrawler crawler;
        protected readonly IWebDriver driver;

        public BaseForFunctionalityClasses(ICrawler crawler, IWebDriver driver)
        {
            this.crawler = crawler;
            this.driver = driver;
        }

        protected ICrawler Execute(Action methodToExecute)
        {
            methodToExecute?.Invoke();
            return crawler;
        }
    }
}
