using MBusReader.Contracts;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace MBusReader.Code
{
    public class SettingsSerial : ISettingsSerial
    {
        private readonly StopBits _stopBits = StopBits.One;
        private readonly string _portName = "/dev/ttyUSB0";
        private readonly Parity _parity = Parity.None;
        private readonly int _dataBits = 8;
        private readonly int _baudRate = 2400;

        
        public string PortName => _portName;
        public int DataBits => _dataBits;
        public int BaudRate => _baudRate;
        public Parity Parity => _parity;
        public StopBits StopBits => _stopBits;

        public SettingsSerial()
        {
        }

        public SettingsSerial(string portName, int dataBits, int baudRate,Parity parity, StopBits stopBits)
        {
            _portName = portName;
            _dataBits = dataBits;
            _stopBits = stopBits;
            _parity = parity;
            _stopBits = stopBits;
            _baudRate = baudRate;
        }
    }
}
