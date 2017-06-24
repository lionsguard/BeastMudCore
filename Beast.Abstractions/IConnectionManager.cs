using System;
using System.Threading.Tasks;

namespace Beast
{
    public interface IConnectionManager
    {
        event EventHandler<IConnection> Connected;
        event EventHandler<IConnection> Disconnected;

        void Add(IConnection connection);
        void Remove(IConnection connection);
        void Clear();

        Task WaitForConnectionsAsync();
    }
}
