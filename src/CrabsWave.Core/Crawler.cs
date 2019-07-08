using System;
using System.Diagnostics.CodeAnalysis;
using CrabsWave.Core.Configurations;
using CrabsWave.Core.LogsReports;
using CrabsWave.Core.Validations;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CrabsWave.Core
{
    public class Crawler : IDisposable
    {
        internal IWebDriver Driver;
        private readonly ILogger<Crawler> Logger;
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
        public Crawler(ILogger<Crawler> logger) => Logger = logger;

        #endregion Constructors

        public Crawler Initializate(Behavior behavior)
        {
            LogManager.Instance.Initializate(Logger, behavior.Verbose);
            LogManager.Instance.ForceLogInformation("Crawler created, starting to configure");
            Capabilities = BehaviorBuilder.Build(behavior);
            Ready = false;

            LogManager.Instance.LogInformation("Checking Webdriver dependencies");
            if (!SeleniumDependencies.CheckLocalWebDriverAvialability())
            {
                Logger.LogCritical("Could not initilization, missing webdriver");
                return this;
            }

            LogManager.Instance.LogInformation("is working after create driver.");
            if (!CreateDriver())
            {
                Logger.LogError("Could not create the driver");
                return this;
            }

            LogManager.Instance.LogInformation("is working before create driver.");
            Ready = true;
            LogManager.Instance.LogInformation("The Crawler is ready to use.");
            Logger.LogInformation("Successful crab initilization");

            return this;
        }

        private bool CreateDriver()
        {
            LogManager.Instance.LogInformation("Initializing the Driver and service");
            (Driver, Service) = Initialization.Create(Capabilities);
            return Driver != null && Service != null;
        }
    }
}
