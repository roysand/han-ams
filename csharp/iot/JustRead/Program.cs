using System;
using System.Globalization;
using System.IO;
using System.Threading;
using MBusReader.Code;
using MBusReader.Contracts;

namespace JustRead
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = $"data{Path.DirectorySeparatorChar}binary-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}.dat";
            Console.WriteLine($"Filename: {fileName}");
            Console.WriteLine("Starting reading data ...!");
            
            Stream stream = new FileStream(fileName,FileMode.Create);
            ISettingsSerial serialSettings = new SettingsSerial();
            if (System.OperatingSystem.IsWindows())
            {
                serialSettings.PortName = "com3";
            }
            
            IMBusReader mbusReader = new ReliableMBusReader(stream, serialSettings);
            // IMBusReader mbusReader = new MBusReader.Code.MBusReader(stream, serialSettings);
            
            mbusReader.Run(true);

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(100);
            }

             mbusReader.Close();
             stream.Close();

            Console.WriteLine("Program end ...!!");
        }
    }
}
