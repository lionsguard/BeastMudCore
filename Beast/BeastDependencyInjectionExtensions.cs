using Beast;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BeastDependencyInjectionExtensions
    {
        public static IBeastBuilder AddBeast(this IServiceCollection services)
        {
            return new BeastBuilder(services);
        }
    }
}
