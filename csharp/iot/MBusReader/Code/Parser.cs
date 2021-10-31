using System;
using System.Collections.Generic;
using System.Linq;
using MBusReader.Contracts;
using MessageParser.Contracts;

namespace MessageParser.Code
{
    public class Parser : IParser
    {
        private IHDLCMessage _hdlcMessage;
        
        public Parser(IHDLCMessage message)
        {
            _hdlcMessage = message;
        }
        public IHDLCMessage Parse(List<byte> data)
        {
            // First elemnt is start mark 7E
            var pkt = BitConverter.ToString(data.ToArray()).Replace("-","");
            Console.WriteLine($"Parser: {pkt}");
            
            return _hdlcMessage;
        }
    }
}