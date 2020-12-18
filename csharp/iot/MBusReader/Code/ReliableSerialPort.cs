using System;
using System.IO.Ports;

namespace ExtendedSerialPort
{
    public class ReliableSerialPort : SerialPort
    {
        public ReliableSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            PortName = portName;
            BaudRate = baudRate;
            DataBits = dataBits;
            Parity = parity;
            StopBits = stopBits;
            Handshake = Handshake.None;
            DtrEnable = true;
            NewLine = Environment.NewLine;
            ReceivedBytesThreshold = 1024;
        }

        public ReliableSerialPort(string portName, int baudRate, Parity parity, int dataBits)
        {
            PortName = portName;
            BaudRate = baudRate;
            Parity = parity;
            DataBits = dataBits;
        }
        new public void Open()
        {
            base.Open();
            ContinuousRead();
        }

        private void ContinuousRead()
        {
            byte[] buffer = new byte[4096];
            Action kickoffRead = null;
            kickoffRead = (Action)(() => BaseStream.BeginRead(buffer, 0, buffer.Length, delegate (IAsyncResult ar)
            {
                try
                {
                    int count = BaseStream.EndRead(ar);
                    byte[] dst = new byte[count];
                    Buffer.BlockCopy(buffer, 0, dst, 0, count);
                    OnDataReceived(dst);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"OptimizedSerialPort exception! {exception.Message}");
                }
                kickoffRead();
            }, null)); kickoffRead();
        }
         public delegate void RoyDataReceivedEventHandler(object sender, DataReceivedArgs e);
        public event RoyDataReceivedEventHandler DataReady;
        public virtual void OnDataReceived(byte[] data)
        {
            var handler = DataReady;
            if (handler != null)
            {
                handler(this, new DataReceivedArgs  { Data = data });
            }
        }
    }

    public class DataReceivedArgs  : EventArgs
    {
        public byte[] Data { get; set; }
    }
}
