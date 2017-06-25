using System;

namespace Beast.CommandLine
{
    public static class ServerExtensions
    {
        public static void Run(this IServer server)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("BEAST");
            Console.WriteLine("(C) 2003-{0} Lionsguard Technologies, LLC.", DateTime.Now.Year);
            Console.WriteLine(new string('=', 70));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ctrl+Shift+Q to shutdown.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Gray;

            var runner = new ServerRunner(server);
            runner.Run(key =>
                {
                    if (key.Key == ConsoleKey.Q
                       && key.Modifiers.HasFlag(ConsoleModifiers.Control)
                       && key.Modifiers.HasFlag(ConsoleModifiers.Shift))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Are you sure you want to shutdown the Beast Server? y/N");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        var input = Console.ReadKey();
                        if (input.Key == ConsoleKey.Y)
                            return true;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Shutdown aborted.");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    return false;
                }).Wait();
        }
    }
}
