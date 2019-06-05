using System;
using System.Diagnostics.CodeAnalysis;
using CrabsWave.Core.Configurations;
using CrabsWave.Core.Functionalities.Elements;
using CrabsWave.Core.Functionalities.Navegation;
using CrabsWave.Core.LogsReports;
using CrabsWave.Core.Validations;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CrabsWave.Core
{
    public class Crawler : ICrawler
    {
        private readonly ILogger<ICrawler> Logger;
        private string[] Capabilities;
        private IWebDriver Driver;
        private ChromeDriverService Service;
        public bool Ready { get; set; }
        private ICrawlerNavigation CrawlerNavigation { get; set; }
        private ICrawlerElements CrawlerElements { get; set; }

        #region IDisposable Support

        private bool disposedValue = false;

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Driver?.Dispose();
                    Driver = null;

                    Service?.Dispose();
                    Service = null;
                }

                disposedValue = true;
            }
        }

        #endregion IDisposable Support

        #region Constructors

        /// <summary>
        /// Creates a new instance of Crawler buiding the behaviors to Webdriver and for the crawler,
        /// also init the driver with all checks
        /// </summary>
        /// <param name="logger">Dependency injection for ILogger</param>
        public Crawler(ILogger<ICrawler> logger) => Logger = logger;

        #endregion Constructors

        public ICrawler Initializate(Behavior behavior)
        {
            LogManager.Initializate(Logger, behavior.Verbose);
            LogManager.ForceLogInformation("Crawler created, starting to configure");
            Capabilities = BehaviorBuilder.Build(behavior);
            Ready = false;

            LogManager.LogInformation("Checking Webdriver dependencies");
            if (!SeleniumDependencies.CheckLocalWebDriverAvialability())
            {
                Logger.LogCritical("Could not initilization, missing webdriver");
                return this;
            }

            if (!CreateDriver())
            {
                Logger.LogError("Could not create the driver");
                return this;
            }

            Ready = true;
            LogManager.LogInformation("The Crawler is ready to use.");
            Logger.LogInformation("Successful crab initilization");

            CrawlerNavigation = new CrawlerNavigation(this, Driver);
            CrawlerElements = new CrawlerElements(this, Driver);
            return this;
        }

        public ICrawlerNavigation Navigation() => CrawlerNavigation;
        public ICrawlerElements Elements() => CrawlerElements;

        private bool CreateDriver()
        {
            LogManager.LogInformation("Initializing the Driver and service");
            (Driver, Service) = Initialization.Create(Capabilities);
            return Driver != null && Service != null;
        }
    }
}
