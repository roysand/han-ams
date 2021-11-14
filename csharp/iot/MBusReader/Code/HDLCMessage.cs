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
        public int DataLen { get; set; }
        public string EpocDateString { get; set; }

        public HDLCHeader()
        {
            
        }
    }
    
    public class HDLCData : IHDLCData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Obis_Code { get; set; }
        public string Unit { get; set; }
        public float Value { get; set; }

        public HDLCData()
        {
            
        }

        public HDLCData(string name, string description, string obisCode, string unit, float value)
        {
            Name = name;
            Description = description;
            Obis_Code = obisCode;
            Unit = unit;
            Value = value;
        }
    }
}