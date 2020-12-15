using System;
using System.Threading;
using MBusReader.Contracts;

namespace JustRead
{
    class Program
    {
        static void Main(string[] args)
        {
            IMBusReader mbusReader = new MBusReader.Code.MBusReader();
            mbusReader.Run();

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(100);
            }
            
            Console.WriteLine("Stop reading data ...!");
        }
    }
}
