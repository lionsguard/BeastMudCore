using Beast.Sockets;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Beast
{
    public static class NetBeastBuilderExtensions
    {
        public static IBeastBuilder AddTcpListener(this IBeastBuilder builder, Action<TcpSettings> configureOptions)
        {
            builder.Services.Configure(configureOptions);

            builder.Services.AddSingleton<IListener, TcpListener>();

            return builder;
        }
    }
}
