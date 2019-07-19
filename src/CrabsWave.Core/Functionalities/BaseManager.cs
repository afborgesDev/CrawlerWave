using Microsoft.Extensions.Logging;

namespace CrabsWave.Core.Functionalities
{
    internal class BaseManager
    {
        public static string LoggerCategory { get; private set; }
        protected readonly ILogger Logger;

        public BaseManager(string loggerCategory, ILogger logger)
        {
            LoggerCategory = loggerCategory;
            Logger = logger;
        }
    }
}
