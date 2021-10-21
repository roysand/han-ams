using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace MBusReader.Contracts
{
    public interface ISettingsSerial
    {
        string PortName { get; set; }
        int DataBits { get; }
        int BaudRate { get; }
        Parity Parity { get; }
        StopBits StopBits { get; }
    }
}
