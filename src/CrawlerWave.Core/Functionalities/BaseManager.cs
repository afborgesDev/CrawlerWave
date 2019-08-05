using Microsoft.Extensions.Logging;

namespace CrawlerWave.Core.Functionalities
{
    internal class BaseManager
    {
        protected readonly ILogger Logger;

        public BaseManager(string loggerCategory, ILogger logger)
        {
            LoggerCategory = loggerCategory;
            Logger = logger;
        }

        public static string LoggerCategory { get; private set; }
    }
}
