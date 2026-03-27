using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Dream.Network;

public class NetTcpServer : INetServer
{
    private readonly TcpListener _server;
    private CancellationTokenSource? _cts;

    public IPEndPoint EndPoint { get; }
    public string Key { get; }
    public int MaxCountClients { get; }
    public bool IsRunning { get; private set; }

    private readonly List<INetClient> _clients = [];
    public IReadOnlyList<INetClient> Clients => _clients.AsReadOnly();

    public event Action<INetClient>? OnClientConnectedEvent;
    public event Action<INetClient>? OnClientDisconnectedEvent;


    public NetTcpServer(int port, int maxCountClients = -1)
    {
        EndPoint = new IPEndPoint(IPAddress.Parse(NetworkSettings.GetLocalIPAddress()), port);
        Key = NetworkSettings.GetLocalKey(EndPoint);
        MaxCountClients = maxCountClients;
        _server = new TcpListener(EndPoint);
    }


    public void Start()
    {
        if (IsRunning) return;
        IsRunning = true;
        _cts = new CancellationTokenSource();
        _server.Start();

        _ = AcceptLoopAsync(_cts.Token);
    }

    private async Task AcceptLoopAsync(CancellationToken token)
    {
        try
        {
            while (IsRunning && !token.IsCancellationRequested)
            {
                TcpClient tcpClient = await _server.AcceptTcpClientAsync(token);

                if (MaxCountClients >= 0 && _clients.Count >= MaxCountClients)
                {
                    tcpClient.Close();
                    continue;
                }

                NetTcpClient client = new(tcpClient);
                lock (_clients) _clients.Add(client);

                client.OnDisconnectedEvent += () =>
                {
                    lock (_clients) _clients.Remove(client);
                    OnClientDisconnectedEvent?.Invoke(client);
                };
                OnClientConnectedEvent?.Invoke(client);
            }
        }
        catch (Exception ex) when (ex is OperationCanceledException || ex is ObjectDisposedException) { /* Штатная остановка сервера */ }
        catch (Exception) { /* Нештатная ситуация (SocketException и т.д.) */ }
        finally
        {
            if (IsRunning)
                Stop();
        }
    }

    public void Stop()
    {
        if (!IsRunning) return;
        IsRunning = false;

        try { _cts?.Cancel(); } catch { }

        lock (_clients)
        {
            foreach (INetClient client in _clients)
                client.Close();
            _clients.Clear();
        }

        _server.Stop();
    }

    public void Dispose()
    {
        Stop();
        GC.SuppressFinalize(this);
    }
}
