using Microsoft.Extensions.Logging;

namespace CrawlerWave.Core.Functionalities
{
    internal class BaseManager
    {
        protected readonly ILogger Logger;

        public BaseManager(string loggerCategory, ILogger logger)
        {
            if (string.IsNullOrWhiteSpace(loggerCategory))
                throw new ArgumentException($"'{nameof(loggerCategory)}' cannot be null or whitespace.", nameof(loggerCategory));

            LoggerCategory = loggerCategory;
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public static string? LoggerCategory { get; private set; }
    }
}
