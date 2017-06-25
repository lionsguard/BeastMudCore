using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
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
        readonly IWorld _world;
        readonly IClock _clock;

        readonly CancellationTokenSource _cancellationSource;

        public Server(
            IServiceProvider services, 
            ILoggerFactory loggerFactory, 
            IConnectionManager connectionManager, 
            IContentProvider contentProvider,
            IConnectionContextFactory contextFactory,
            IWorld world,
            IClock clock)
        {
            _services = services;
            _logger = loggerFactory.CreateLogger<Server>();
            _connections = connectionManager;
            _content = contentProvider;
            _contextFactory = contextFactory;
            _world = world;
            _clock = clock;

            _cancellationSource = new CancellationTokenSource();
        }

        public void Start()
        {
            _logger.LogInformation("Starting BEAST...");

            InitWorld();
            RegisterEvents();
            StartListeners();

            Run();

            _logger.LogInformation("Started BEAST...");
        }

        public void Stop()
        {
            _logger.LogInformation("Stopping BEAST...");
            
            _cancellationSource.Cancel();

            StopListeners();
            UnregisterEvents();
            ShutdownWorld();

            _logger.LogInformation("Stopped BEAST...");
        }

        void Run()
        {
            Task.Run(async () =>
            {
                _clock.Start();

                while (!_cancellationSource.IsCancellationRequested)
                {
                    await _connections.WaitForConnectionsAsync();

                    _clock.Update();

                    _world.Update(_clock.CurrentTime);
                }
            });
        }

        void InitWorld()
        {
            if (_world == null)
                throw new InvalidOperationException("An instance of IWorld could not be found. Set the world instance using the 'UseWorld' extension on the IBeastBuilder object.");

            _world.Initialize();
        }

        void ShutdownWorld()
        {
            _world?.Shutdown();
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
