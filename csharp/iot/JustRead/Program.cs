using System;
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
                
            }
            
            Console.WriteLine("Stop reading data ...!");
        }
    }
}
