using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Beast.ConnectionContexts
{
    public abstract class ConnectionContextBase : IConnectionContext
    {
        protected IServiceProvider Services { get; }
        protected IConnection Connection { get; }
        protected IContentProvider Content { get; }
        protected IConnectionContextFactory ContextFactory { get; }

        protected ConnectionContextBase(IServiceProvider services, IConnection connection)
        {
            Services = services;
            Connection = connection;

            Content = Services.GetService<IContentProvider>();
            ContextFactory = Services.GetService<IConnectionContextFactory>();

            Task.Run(async () => await Enter());
        }

        public abstract Task ProcessInput(string input);

        protected abstract Task Enter();
    }
}
