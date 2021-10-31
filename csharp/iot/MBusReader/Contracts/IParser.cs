using System.Collections.Generic;
using MBusReader.Contracts;

namespace MessageParser.Contracts
{
    public interface IParser
    {
        IHDLCMessage Parse(List<byte> data);
    }
}