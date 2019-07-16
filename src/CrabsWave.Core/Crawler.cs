using System;
using System.Diagnostics.CodeAnalysis;
using CrabsWave.Core.Configurations;
using CrabsWave.Core.Validations;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CrabsWave.Core
{
    public class Crawler : IDisposable
    {
        public const string LoggerCategory = "CrawlerWave.Core";
        internal readonly ILoggerFactory LoggerFactory;
        internal IWebDriver Driver;
        private readonly ILogger Logger;
        private string[] Capabilities;
        private ChromeDriverService Service;
        public bool Ready { get; set; }

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
        public Crawler(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
            Logger = CreateLogger(LoggerCategory);
        }

        #endregion Constructors

        public Crawler Initializate(Behavior behavior)
        {
            Logger.LogInformation("Crawler created, starting to configure");
            Capabilities = BehaviorBuilder.Build(behavior);
            Ready = false;

            Logger.LogInformation("Checking Webdriver dependencies");
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
            Logger.LogInformation("The Crawler is ready to use.");
            Logger.LogInformation("Successful crab initilization");
            return this;
        }

        public ILogger CreateLogger(string category) => LoggerFactory.CreateLogger(category);

        private bool CreateDriver()
        {
            (Driver, Service) = Initialization.Create(Capabilities);
            if (Driver == null || Service == null)
            {
                Logger.LogError("Could not create service or driver");
                return false;
            }

            return true;
        }
    }
}
