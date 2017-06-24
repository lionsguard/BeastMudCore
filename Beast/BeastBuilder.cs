using Beast.ConnectionContexts;
using Microsoft.Extensions.DependencyInjection;

namespace Beast
{
    public class BeastBuilder : IBeastBuilder
    {
        public IServiceCollection Services { get; }

        public BeastBuilder(IServiceCollection services)
        {
            Services = services;

            Services.AddOptions();

            Services.AddSingleton<IServer, Server>();
            Services.AddSingleton<IConnectionManager, ConnectionManager>();
            Services.AddSingleton<IContentProvider, ContentProvider>();
            Services.AddSingleton<IConnectionContextFactory, ConnectionContextFactory>();
        }
    }
}
