using System;
using System.Collections.Generic;
using System.Net;

namespace Dream.Network;

public interface INetServer : IDisposable
{
    IPEndPoint EndPoint { get; }
    string Key { get; }
    bool IsRunning { get; }
    IReadOnlyList<INetClient> Clients { get; }

    event Action<INetClient>? OnClientConnectedEvent;
    event Action<INetClient>? OnClientDisconnectedEvent;

    void Start();
    void Stop();
}
