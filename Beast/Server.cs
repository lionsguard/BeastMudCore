using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Beast
{
    public class Server : IServer
    {
        readonly IServiceProvider _services; 
        readonly ILogger _logger;
        readonly IConnectionManager _connections;
        readonly IContentProvider _content;
        readonly IConnectionContextFactory _contextFactory;

        public Server(
            IServiceProvider services, 
            ILoggerFactory loggerFactory, 
            IConnectionManager connectionManager, 
            IContentProvider contentProvider,
            IConnectionContextFactory contextFactory)
        {
            _services = services;
            _logger = loggerFactory.CreateLogger<Server>();
            _connections = connectionManager;
            _content = contentProvider;
            _contextFactory = contextFactory;
        }

        public void Start()
        {
            _logger.LogInformation("Starting BEAST...");

            RegisterEvents();
            StartListeners();

            _logger.LogInformation("Started BEAST...");
        }

        public void Stop()
        {
            _logger.LogInformation("Stopping BEAST...");

            StopListeners();
            UnregisterEvents();

            _logger.LogInformation("Stopped BEAST...");
        }

        void StartListeners()
        {
            Task.Run(async () =>
            {
                var listeners = _services.GetServices<IListener>();
                foreach (var listener in listeners)
                {
                    await listener.Start();
                }
            });
        }

        void StopListeners()
        {
            Task.Run(async () =>
            {
                var listeners = _services.GetServices<IListener>();
                foreach (var listener in listeners)
                {
                    await listener.Stop();
                }
            });
        }

        void RegisterEvents()
        {
            _connections.Connected += OnConnectionConnected;
            _connections.Disconnected += OnConnectionDisconnected;
        }

        void UnregisterEvents()
        {
            _connections.Clear();
            _connections.Connected -= OnConnectionConnected;
            _connections.Disconnected -= OnConnectionDisconnected;
        }

        async void OnConnectionConnected(object sender, IConnection conn)
        {
            await conn.SendAsync(_content.GetText(ContentKeys.ConnectionConnected));
            conn.Context = _contextFactory.CreateInitialContext(conn);
        }

        async void OnConnectionDisconnected(object sender, IConnection conn)
        {
            await conn.SendAsync(_content.GetText(ContentKeys.ConnectionDisonnected));
        }
    }
}
