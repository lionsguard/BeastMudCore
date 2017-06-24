using System;

namespace Beast
{
    public interface IConnectionContextFactory
    {
        IConnectionContext CreateInitialContext(IConnection connection);
        IConnectionContext CreateAuthenticationContext(IConnection connection);
        IConnectionContext CreateNewCharacterContext(IConnection connection);
        IConnectionContext CreatePlayContext(IConnection connection);

        IConnectionContext CreateConnectionContext(Type type, IConnection connection);
        TConnectionContext CreateConnectionContext<TConnectionContext>(IConnection connection) where TConnectionContext : class,IConnectionContext;
    }
}
