using System;
using System.Net;
using System.Threading.Tasks;

namespace Dream.Network;

public interface INetClient : IDisposable
{
    IPAddress IP { get; }
    bool IsConnected { get; }
    object? Data { get; set; }

    event Action<string>? OnMessageReceived;
    event Action? OnDisconnectedEvent;

    Task<bool> SendAsync(string message);
    void Close();
}
