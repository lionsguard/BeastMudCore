using System;

namespace Beast.ConnectionContexts
{
    public class ConnectionContextFactory : IConnectionContextFactory
    {
        readonly IServiceProvider _services;

        public ConnectionContextFactory(IServiceProvider services)
        {
            _services = services;
        }

        public IConnectionContext CreateInitialContext(IConnection connection)
        {
            return new InitialConnectionContext(_services, connection);
        }

        public IConnectionContext CreateAuthenticationContext(IConnection connection)
        {
            throw new NotImplementedException();
        }

        public IConnectionContext CreateNewCharacterContext(IConnection connection)
        {
            return new NewCharacterConnectionContext(_services, connection);
        }

        public IConnectionContext CreatePlayContext(IConnection connection)
        {
            throw new NotImplementedException();
        }

        public TConnectionContext CreateConnectionContext<TConnectionContext>(IConnection connection)
             where TConnectionContext : class, IConnectionContext
        {
            return CreateConnectionContext(typeof(TConnectionContext), connection) as TConnectionContext; 
        }

        public IConnectionContext CreateConnectionContext(Type type, IConnection connection)
        {
            return Activator.CreateInstance(type, _services, connection) as IConnectionContext;
        }
    }
}
