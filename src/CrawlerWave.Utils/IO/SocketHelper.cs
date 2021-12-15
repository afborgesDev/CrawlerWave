using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace CrawlerWave.Utils.IO;

public static class SocketHelper
{
    public static int GetNewSocketPort()
    {
        var tcpListener = new TcpListener(IPAddress.Loopback, 0);
        tcpListener.Start();
        var port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
        tcpListener.Stop();
        return port;
    }

    public static int[] GetSocketPortsInUse() => IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().Select(s => s.LocalEndPoint.Port).ToArray();

    public static int GetRandomPortToTry() => GetRandomPort();

    public static int GetRandomPort()
    {
        var byteArrayNumber = RandomNumberGenerator.GetBytes(5);
        var number = BitConverter.ToInt32(byteArrayNumber, 0);
        if (number < 0) number *= -1;
        return int.Parse(number.ToString().Substring(0, 5));
    }
}
