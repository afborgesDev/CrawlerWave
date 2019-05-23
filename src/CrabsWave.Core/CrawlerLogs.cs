using Microsoft.Extensions.Logging;

namespace CrabsWave.Core
{
    public partial class Crawler
    {
        public void Loginformation(string message)
        {
            if (Verbose)
                Logger.LogInformation(message);
        }
    }
}
