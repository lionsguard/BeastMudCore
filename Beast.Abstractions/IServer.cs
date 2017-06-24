using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beast
{
    public interface IServer
    {
        void Start();
        void Stop();

        Task ProcessInput(IConnection connection, string input);
    }
}
