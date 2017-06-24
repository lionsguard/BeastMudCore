using Microsoft.Extensions.DependencyInjection;

namespace Beast
{
    public interface IBeastBuilder
    {
        IServiceCollection Services { get; }
    }
}
