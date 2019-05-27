using System;
using System.Diagnostics.CodeAnalysis;
using CrabsWave.Core.Configurations;
using CrabsWave.Core.Validations;
using Microsoft.Extensions.Logging;

namespace CrabsWave.Core
{
    public partial class Crawler : ICrawler
    {
        private readonly ILogger<ICrawler> Logger;
        private string[] Capabilities;
        private bool Verbose;

        #region IDisposable Support
        private bool disposedValue = false;

        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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

            Loginformation("Checking Webdriver dependencies");
            if (!SeleniumDependencies.CheckLocalWebDriverAvialability())
            {
                Logger.LogCritical("Could not initilization, missing webdriver");
                return this;
            }


            //Initializate driver
            //For now just initilizate sigle
            //after initializate for grid

            Logger.LogInformation("Successful crab initilization");
            return this;
        }

        
    }
}
