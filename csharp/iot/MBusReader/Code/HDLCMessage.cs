using System;
using System.Collections.Generic;
using MBusReader.Contracts;

namespace MBusReader.Code
{
    public class HDLCMessage : IHDLCMessage
    {
        public IHDLCHeader Header { get; set; }
        public IList<IHDLCData> Data { get; set; }

        public HDLCMessage()
        {
            Data = new List<IHDLCData>();
            Header = new HDLCHeader();
        }
    }
    
    public class HDLCHeader : IHDLCHeader
    {
        public int Hdlc_Length { get; set; }
        public int DataType { get; set; }
        public int ObjectCount { get; set; }
        public int SecondsSinceEpoc { get; set; }
        public DateTime Timestamp { get; set; }

        public HDLCHeader()
        {
            
        }
    }
    
    public class HDLCData : IHDLCData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ObisCode { get; set; }
        public string Unit { get; set; }
        public decimal Value { get; set; }

        public HDLCData()
        {
            Value = -1;
        }

        public HDLCData(string name, string description, string obisCode, string unit, decimal value)
        {
            Name = name;
            Description = description;
            ObisCode = obisCode;
            Unit = unit;
            Value = value;
        }
    }
}