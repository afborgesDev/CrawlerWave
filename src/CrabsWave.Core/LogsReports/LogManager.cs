using System;
using CrabsWave.Core.ErrorHandler;
using Microsoft.Extensions.Logging;

namespace CrabsWave.Core.LogsReports
{
    public static class LogManager
    {
        private static ILogger<ICrawler> Logger { get; set; }
        private static bool Verbose { get; set; }

        public static void Initializate(ILogger<ICrawler> logger, bool verbose)
        {
            Logger = logger;
            Verbose = verbose;
        }

        public static void LogInformation(string message)
        {
            if (Verbose)
                ForceLogInformation(message);
        }

        public static void ForceLogInformation(string message)
        {
            CheckLoggerAvaliable();
            Logger.LogInformation(message);
        }

        public static void LogError(string message)
        {
            CheckLoggerAvaliable();
            Logger.LogError(message);
        }

        public static void LogError(string message, Exception ex)
        {
            CheckLoggerAvaliable();
            Logger.LogError(ex, message);
        }

        private static void CheckLoggerAvaliable()
        {
            if (Logger == null)
                throw new CrawlerBaseException("Should initilizate the logger");
        }
    }
}
