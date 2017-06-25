using System.Threading.Tasks;

namespace Beast
{
    public interface IConnectionContext
    {
        Task ProcessInput(string input);
    }
}
