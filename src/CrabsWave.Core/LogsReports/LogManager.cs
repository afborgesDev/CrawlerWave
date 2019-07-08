using System;
using CrabsWave.Core.ErrorHandler;
using Microsoft.Extensions.Logging;

namespace CrabsWave.Core.LogsReports
{
    public class LogManager
    {
        private static readonly object objecLock = new object();

        public static LogManager Instance {
            get {
                lock (objecLock)
                {
                    if (InternalInstance == null)
                        InternalInstance = new LogManager();

                    return InternalInstance;
                }
            }
        }

        public ILogger<Crawler> Logger { get; private set; }
        private static LogManager InternalInstance { get; set; }
        private bool Verbose { get; set; }

        public void Initializate(ILogger<Crawler> logger, bool verbose)
        {
            Logger = logger;
            Verbose = verbose;
        }

        public void LogInformation(string message)
        {
            if (Verbose)
                ForceLogInformation(message);
        }

        public void ForceLogInformation(string message)
        {
            CheckLoggerAvaliable();
            Logger.LogInformation(message);
        }

        public void LogError(string message)
        {
            CheckLoggerAvaliable();
            Logger.LogError(message);
        }

        public void LogError(string message, Exception ex)
        {
            CheckLoggerAvaliable();
            Logger.LogError(ex, message);
        }

        private void CheckLoggerAvaliable()
        {
            if (Logger == null)
                throw new CrawlerBaseException("Should initilizate the logger");
        }
    }
}
