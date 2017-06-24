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
        }
    }
}
