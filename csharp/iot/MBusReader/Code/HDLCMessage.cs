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
        }
    }
}