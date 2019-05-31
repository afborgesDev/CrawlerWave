using System;
using System.Diagnostics.CodeAnalysis;
using CrabsWave.Core.Configurations;
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
        private bool Verbose;
        private IWebDriver Driver = null;
        private ChromeDriverService Service = null;
        public bool Ready { get; set; }

        #region IDisposable Support
        private bool disposedValue = false;

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

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of Crawler buiding the behaviors to Webdriver and for the crawler, also init the driver with all checks
        /// </summary>
        /// <param name="logger">Dependency  injection for ILogger</param>
        public Crawler(ILogger<ICrawler> logger) => Logger = logger;
        #endregion

        public ICrawler Initializate(Behavior behavior)
        {
            Logger.LogInformation("Crawler created, starting to configure");
            Capabilities = BehaviorBuilder.Build(behavior);
            Verbose = behavior.Verbose;
            Ready = false;

            Loginformation("Checking Webdriver dependencies");
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
            Loginformation("The Crawler is ready to use.");
            Logger.LogInformation("Successful crab initilization");
            return this;
        }

        public void Loginformation(string message)
        {
            if (Verbose)
                Logger.LogInformation(message);
        }

        private bool CreateDriver()
        {
            Loginformation("Initializing the Driver and service");
            (Driver, Service) = Initialization.Create(Capabilities);
            return Driver != null && Service != null;
        }
    }
}
