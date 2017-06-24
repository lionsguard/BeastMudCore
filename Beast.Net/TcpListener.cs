using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Beast.Net
{
    public class TcpListener : IListener
    {
        TcpSettings _settings;
        Socket _listener;
        CancellationTokenSource _cancellationSource;
        ILogger _logger;
        //IConnectionManager _connectionManager;
        //ISimulator _simulator;
        readonly List<TcpConnection> _connections = new List<TcpConnection>();

        public TcpListener(ILoggerFactory loggerFactory, IOptions<TcpSettings> settings)
        {
            _settings = settings.Value;
            _logger = loggerFactory.CreateLogger<TcpListener>();
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public async Task Start()
        {
            await ListenAsync(_settings.Port, _cancellationSource);
        }

        public Task Stop()
        {
            _cancellationSource.Cancel(false);
            return Task.CompletedTask;
        }

        public async Task ListenAsync(int port, CancellationTokenSource cancellationSource)
        {
            try
            {
                var ep = new IPEndPoint(IPAddress.Any, port);
                _listener.Bind(ep);
                _listener.Listen(100);

                _logger.LogInformation("Started Socket Listener on '{0}'", ep.ToString());

                while (!cancellationSource.Token.IsCancellationRequested)
                {
                    var client = await _listener.AcceptAsync();

                    _logger.LogInformation("Accepted a socket connection from '{0}'.", client.RemoteEndPoint.ToString());

                    //var connection = new SocketConnection(client, _logger, _simulator);
                    //_connections.Add(connection);

                    //_connectionManager.Add(connection);

                    //connection.BeginReceive();
                }
            }
            catch (Exception ex)
            {
                cancellationSource.Cancel(false);
                throw ex;
            }

            foreach (var conn in _connections)
            {
                //_connectionManager.Remove(conn);
                await conn.CloseAsync();
            }

            if (_listener != null)
            {
                _listener.Shutdown(SocketShutdown.Both);
                _listener.Dispose();
                _listener = null;
            }
        }
    }
}
