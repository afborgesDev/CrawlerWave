using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
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
                var portUse = GetRandomPortToTry();
                if (Array.IndexOf(GetSocketPortsInUse(), portUse) < 0) return portUse;
                Thread.Sleep(TimeSpan.FromSeconds(SecondsToWaitNewPort));
            }

            return DefaultInvalidSocketPort;
        }

        public static int[] GetSocketPortsInUse() => IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().Select(s => s.LocalEndPoint.Port).ToArray();

        public static int GetRandomPortToTry() => GetRandomPort();

        public static int GetRandomPort()
        {
            var provider = new RNGCryptoServiceProvider();
            var byteArrayNumber = new byte[5];
            provider.GetBytes(byteArrayNumber);
            var number = BitConverter.ToInt32(byteArrayNumber, 0);
            if (number < 0) number *= -1;
            return int.Parse(number.ToString().Substring(0, 5));
        }
    }
}
