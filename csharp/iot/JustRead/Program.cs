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
            Stream stream = new FileStream($"data{Path.PathSeparator}binary-{DateTime.Now.ToString("yyyy-MM-dd-HH24-mm")}.dat",FileMode.Create);
            IMBusReader mbusReader = new MBusReader.Code.MBusReader(stream);
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
