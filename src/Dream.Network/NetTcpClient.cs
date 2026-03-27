using System;
using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dream.Network;

public class NetTcpClient : INetClient
{
    private readonly TcpClient _client;
    private readonly NetworkStream _stream;
    private bool _isListening;
    private int _isClosed;

    public IPAddress IP { get; }
    public object? Data { get; set; }

    public event Action<string>? OnMessageReceived;
    public event Action? OnDisconnectedEvent;

    public bool IsConnected => _client.Client?.Connected == true && _isClosed == 0;


    internal NetTcpClient(TcpClient client)
    {
        _client = client;
        _stream = _client.GetStream();
        IP = GetIEndPoint().Address;

        _ = StartListeningAsync();
    }

    public NetTcpClient(string key) : this(NetworkSettings.ConvertKey(key)) { }
    public NetTcpClient(IPEndPoint endPoint)
    {
        _client = new TcpClient(AddressFamily.InterNetwork);

        if (!_client.ConnectAsync(endPoint.Address, endPoint.Port).Wait(NetworkSettings.Timeout))
        {
            _client.Dispose();
            throw new TimeoutException($"Timeout exception, elapsed {NetworkSettings.Timeout / 1000f:F2} seconds.");
        }

        _stream = _client.GetStream();
        IP = GetIEndPoint().Address;

        _ = StartListeningAsync();
    }

    private IPEndPoint GetIEndPoint() =>
        (IPEndPoint?)_client.Client.RemoteEndPoint ??
        throw new InvalidOperationException("Unable to determine remote endpoint.");


    public async Task<bool> SendAsync(string message)
    {
        if (!IsConnected || string.IsNullOrEmpty(message))
            return false;

        try
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] lengthPrefix = new byte[4];

            BinaryPrimitives.WriteInt32BigEndian(lengthPrefix, messageBytes.Length);

            await _stream.WriteAsync(lengthPrefix);
            await _stream.WriteAsync(messageBytes);
            return true;
        }
        catch
        {
            Close();
            return false;
        }
    }

    private async Task StartListeningAsync()
    {
        if (_isListening)
            return;
        _isListening = true;

        try
        {
            while (IsConnected)
            {
                string? message = await ReceiveAsync();
                if (string.IsNullOrEmpty(message))
                    break;
                else
                    OnMessageReceived?.Invoke(message);
            }
        }
        catch (Exception) { }
        finally
        {
            _isListening = false;
            Close();
        }
    }

    private async Task<string?> ReceiveAsync()
    {
        try
        {
            byte[] lengthBuffer = new byte[4];
            if (!await ReadExactAsync(lengthBuffer, 4))
                return null;

            int messageLength = BinaryPrimitives.ReadInt32BigEndian(lengthBuffer);

            byte[] messageBuffer = new byte[messageLength];
            if (!await ReadExactAsync(messageBuffer, messageLength))
                return null;

            return Encoding.UTF8.GetString(messageBuffer);
        }
        catch { return null; }
    }

    private async Task<bool> ReadExactAsync(byte[] buffer, int count)
    {
        int totalRead = 0;
        while (totalRead < count)
        {
            int read = await _stream.ReadAsync(buffer.AsMemory(totalRead, count - totalRead));

            if (read == 0)
                return false;

            totalRead += read;
        }
        return true;
    }

    public void Close()
    {
        if (Interlocked.Exchange(ref _isClosed, 1) == 1)
            return;

        _stream?.Close();
        _client?.Close();
        OnDisconnectedEvent?.Invoke();
    }

    public void Dispose()
    {
        Close();
        GC.SuppressFinalize(this);
    }
}
