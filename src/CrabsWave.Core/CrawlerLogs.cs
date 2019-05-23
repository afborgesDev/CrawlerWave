using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CrabsWave.Core
{
    public partial class Crawler
    {
        public async void LoginformationAsync(string message) => await Task.Factory.StartNew(() =>
                                                                     {
                                                                         if (Verbose)
                                                                             Logger.LogInformation(message);
                                                                     }).ConfigureAwait(false);
    }
}
