﻿using OpenQA.Selenium;

namespace CrabsWave.Core.Base
{
    public class BaseForFunctionalityClasses
    {
        protected readonly ICrawler crawler;
        protected readonly IWebDriver driver;

        public BaseForFunctionalityClasses(ICrawler crawler, IWebDriver driver) {
            this.crawler = crawler;
            this.driver = driver;
        }
    }
}
