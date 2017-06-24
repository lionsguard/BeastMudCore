using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Beast.Net
{
    public static class NetBeastBuilderExtensions
    {
        public static IBeastBuilder AddTcpListener(this IBeastBuilder builder, Action<TcpSettings> configureOptions)
        {
            builder.Services.Configure(configureOptions);

            return builder;
        }
    }
}
