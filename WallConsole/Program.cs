using System;

namespace WallConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press x to exit....");
            var inputHandler= new InputHandler();
            var input = String.Empty;
            while (input != "x")
            {
                input = Console.ReadLine();
                var output=inputHandler.AcceptInput(input);
                foreach (var line in output)
                {
                    Console.WriteLine(line);
                }

            }
        }
    }
}
