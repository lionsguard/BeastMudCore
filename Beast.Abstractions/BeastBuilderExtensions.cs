using Microsoft.Extensions.DependencyInjection;
using System;

namespace Beast
{
    public static class BeastBuilderExtensions
    {
        public static IBeastBuilder Configure<TOptions>(this IBeastBuilder builder, Action<TOptions> configureOptions)
            where TOptions : class
        {
            builder.Services.Configure(configureOptions);
            return builder;
        }
    }
}
