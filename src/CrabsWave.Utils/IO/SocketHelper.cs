using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;

namespace CrabsWave.Utils.IO
{
    public static class SocketHelper
    {
        private const int MaxNewSocketPortAttempts = 10;
        private const int DefaultInvalidSocketPort = -1;
        private const int SecondsToWaitNewPort = 5;

        public static int GetNewSocketPort()
        {
            for (var i = 0; i <= MaxNewSocketPortAttempts; i++)
            {
                try
                {
                    var portUse = GetRandomPortToTry();
                    if (Array.IndexOf(GetSocketPortsInUse(), portUse) < 0) return portUse;
                }
                catch
                {
                    Thread.Sleep(TimeSpan.FromSeconds(SecondsToWaitNewPort));
                }
            }

            return DefaultInvalidSocketPort;
        }

        public static int[] GetSocketPortsInUse() => IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().Select(s => s.LocalEndPoint.Port).ToArray();
        public static int GetRandomPortToTry() => int.Parse(new Random(DateTime.Now.Millisecond).Next().ToString().Substring(0, 4));
    }
}
