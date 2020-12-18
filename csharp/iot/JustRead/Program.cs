using System;
using System.IO;
using System.Threading;
using MBusReader.Contracts;

namespace JustRead
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = $"data{Path.DirectorySeparatorChar}binary-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}.dat";
            Console.WriteLine($"Filename: {fileName}");
            
            Stream stream = new FileStream(fileName,FileMode.Create);
            IMBusReader mbusReader = new MBusReader.Code.ReliableMBusReader(stream);
            mbusReader.Run();

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(100);
            }
            
            Console.WriteLine("Stop reading data ...!");
            stream.Close();
        }
    }
}
