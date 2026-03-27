using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Dream.Network;

public static class NetworkSettings
{
    public static int Timeout { get; set; } = 3000;
    private const string CHARECTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string GetLocalIPAddress()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
            if (ip.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip))
                return ip.ToString();

        throw new InvalidOperationException("No network adapters with an IPv4 address in the system!");
    }

    public static string GetLocalKey(IPEndPoint endPoint)
    {
        string key = "";
        byte[] bytes = endPoint.Address.GetAddressBytes();
        long nbrKey = ((long)bytes[0] << 40) | ((long)bytes[1] << 32) |
                      ((long)bytes[2] << 24) | ((long)bytes[3] << 16) | (long)endPoint.Port;

        while (nbrKey != 0)
        {
            key += CHARECTERS[(int)(nbrKey % CHARECTERS.Length)];
            nbrKey /= CHARECTERS.Length;
        }
        return new string([.. key.Reverse()]);
    }

    public static IPEndPoint ConvertKey(string key)
    {
        long nbrKey = 0;
        foreach (char c in key)
        {
            nbrKey *= CHARECTERS.Length;
            nbrKey += CHARECTERS.IndexOf(c);
        }

        IPAddress ip = IPAddress.Parse($"{(nbrKey >> 40) & 0xFF}.{(nbrKey >> 32) & 0xFF}.{(nbrKey >> 24) & 0xFF}.{(nbrKey >> 16) & 0xFF}");
        int port = (int)(nbrKey % (1 << 16));
        return new IPEndPoint(ip, port);
    }
}
