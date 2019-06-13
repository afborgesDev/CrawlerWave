using System;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities.Base
{
    public class BaseForFunctionalityClasses
    {
        protected readonly Crawler crawler;
        protected readonly IWebDriver driver;

        public BaseForFunctionalityClasses(Crawler crawler, IWebDriver driver)
        {
            this.crawler = crawler;
            this.driver = driver;
        }

        protected Crawler Execute(Action methodToExecute)
        {
            methodToExecute?.Invoke();
            return crawler;
        }
    }
}
