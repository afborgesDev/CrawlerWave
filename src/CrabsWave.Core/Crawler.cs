using System;
using System.Diagnostics.CodeAnalysis;
using CrabsWave.Core.Configurations;
using Microsoft.Extensions.Logging;

namespace CrabsWave.Core
{
    public partial class Crawler : ICrawler
    {
        private readonly ILogger<Crawler> Logger;
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
                    // TODO: dispose managed state (managed objects).
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
        public Crawler(ILogger<Crawler> logger) => Logger = logger;
        #endregion

        public void Initializate(Behavior behavior)
        {
            Capabilities = BehaviorBuilder.Build(behavior);
            Verbose = behavior.Verbose;

            //Check driver dependencys
            //Initializate driver
            //For now just initilizate sigle
            //after initializate for grid
        }
    }
}
