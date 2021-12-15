using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MBusReader.Contracts
{
    public interface IHDLCMessage
    {
        IHDLCHeader Header { get; set; }
        IList<IHDLCData> Data { get; set; }
    }

    public interface IHDLCHeader
    {
        int Hdlc_Length { get; set; }
        int DataType { get; set; }
        int ObjectCount { get; set; }
        int SecondsSinceEpoc{ get; set; }
        DateTime Timestamp { get; set; }
    }

    public interface IHDLCData
    {
        string Name { get; set; }
        string Description { get; set; }
        string ObisCode { get; set; }
        string Unit { get; set; }
        float Value { get; set; }
    }
}
