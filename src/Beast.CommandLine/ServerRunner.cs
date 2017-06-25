using System;
using System.Threading.Tasks;

namespace Beast.CommandLine
{
    internal class ServerRunner
    {
        readonly IServer _server;
        readonly AsyncManualResetEvent _wait;

        public ServerRunner(IServer server)
        {
            _server = server;
            _wait = new AsyncManualResetEvent();
        }

        public async Task Run(Func<ConsoleKeyInfo, bool> processKey)
        {
            var serverTask = WaitForServer();
            var inputTask = WaitForInput(processKey);

            await Task.WhenAny(inputTask, serverTask);
        }

        Task WaitForServer()
        {
            return Task.Run(async () =>
            {
                _server.Start();

                await _wait.WaitAsync();

                _server.Stop();
            });
        }

        Task WaitForInput(Func<ConsoleKeyInfo, bool> processKey)
        {
            return Task.Run(() =>
            {
                do
                {
                    var key = Console.ReadKey();
                    if (processKey(key))
                    {
                        _wait.Set();
                        break;
                    }
                } while (true);
            });
        }
    }
}
