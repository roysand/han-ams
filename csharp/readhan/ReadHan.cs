using System;
using System.IO.Ports;
using System.Threading;

namespace readhan
{
    public class ReadHan
    {
        public string Port = "/dev/ttyUSB0";
        public int BaudRate = 2400;
        public Parity Parity = Parity.None;
        public int Bits = 8;
        public StopBits StopBits = StopBits.One;
            
        private SerialPort _port;
        
        public ReadHan()
        {
            _port = new SerialPort(Port, BaudRate, Parity,Bits,StopBits);
        }

        public void Read()
        {
            _port.Open();

            while (!Console.KeyAvailable)
            {
                int bytesAvailable = _port.BytesToRead;
                char[] recBuf = new char[bytesAvailable];
                _port.Read(recBuf, 0, bytesAvailable);

                Console.WriteLine($"Bytes read: {bytesAvailable}");
                for (int index = 0; index < bytesAvailable; index++)
                {
                    System.Console.Write(recBuf[index]);
                }
                System.Console.WriteLine();
                Thread.Sleep(100);
            }
            
            _port.Close();
            _port.Dispose();
        }
    }
}