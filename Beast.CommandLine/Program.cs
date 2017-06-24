using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Beast.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .AddCommandLine(args)
                   .AddEnvironmentVariables()
                   .Build();

            var services = new ServiceCollection()
                .AddLogging();

            var beast = services.AddBeast()
                .AddTcpListener(options =>
                {
                    options.Port = 4000;
                })
                .Configure<ContentOptions>(options =>
                {
                    options.RootPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\beastmud_content");
                });

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>()
                .AddConsole();

            var server = serviceProvider.GetService<IServer>();

            server.Run();

        }
    }
}