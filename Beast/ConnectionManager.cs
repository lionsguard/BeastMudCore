using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Beast
{
    public class ConnectionManager : IConnectionManager
    {
        public event EventHandler<IConnection> Connected = delegate { };
        public event EventHandler<IConnection> Disconnected = delegate { };

        ILogger _logger;
        AsyncManualResetEvent _waitEvent;
        readonly ConcurrentDictionary<Guid, IConnection> _connections = new ConcurrentDictionary<Guid, IConnection>();

        public ConnectionManager(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<IConnectionManager>();
            _waitEvent = new AsyncManualResetEvent();
        }

        public void Add(IConnection connection)
        {
            _connections[connection.Id] = connection;
            _logger.LogInformation("Connection '{0}' added.", connection.Id);
            _waitEvent.Set();

            Connected(this, connection);
        }

        public void Remove(IConnection connection)
        {
            if (_connections.TryRemove(connection.Id, out IConnection removed))
            {
                Disconnected(this, removed);
                removed.CloseAsync();
                _logger.LogInformation("Connection '{0}' removed.", removed.Id);
            }

            if (_connections.Count == 0)
                _waitEvent.Reset();
        }

        public void Clear()
        {
            var connections = _connections.Values.ToList();
            connections.ForEach(c => Remove(c));
            _connections.Clear();
        }

        public Task WaitForConnectionsAsync()
        {
            return _waitEvent.WaitAsync();
        }
    }
}
