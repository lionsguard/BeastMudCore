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

        public static IBeastBuilder UseWorld<TWorld>(this IBeastBuilder builder)
            where TWorld : class, IWorld
        {
            builder.Services.AddSingleton<IWorld, TWorld>();
            return builder;
        }
    }
}
