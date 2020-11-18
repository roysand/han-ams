using System;
using System.IO;
using ParseMessages.Aidon;

namespace ParseMessages
{
    class Program
    {
        static void Main(string[] args)
        {
            // Args[0] name of file to read
            if ((args == null) || (args.Length != 1))
            {
                Console.WriteLine("Usage: Error missing command line parameter filename!");
                Console.WriteLine($"Usage: {System.Reflection.Assembly.GetExecutingAssembly().GetName().ToString()} filename");
                return;
            }

            var fileName = args[0];
            AidonMessageParser parser = new AidonMessageParser();
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var result = parser.Run(fs);
        }
    }
}
