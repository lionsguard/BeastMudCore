using System.Threading.Tasks;

namespace Beast
{
    public interface IListener
    {
        Task Start();
        Task Stop();
    }
}
