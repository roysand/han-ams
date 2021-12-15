using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBusReader.Code;
using MBusReader.Contracts;
using MessageParser.Contracts;

namespace MessageParser.Code
{
    public class Parser : IParser
    {
        // Number of objects in known frames
        private const int OBJECTS_2P5SEC = 1;
        private const int OBJECTS_10SEC = 12;
        private const int OBJECTS_1HOUR = 17;
        
        // Obis datatypes
        private const byte TYPE_STRING = 0x0a;
        private const byte TYPE_UINT32 = 0x06;
        private const byte TYPE_INT16 = 0x10;
        private const byte TYPE_OCTETS = 0x09;
        private const byte TYPE_UINT16 = 0x12;
        
        static private readonly int OBIS_CODE_START = 23;
        static private readonly int VALUE_START = OBIS_CODE_START + 7;
        private IHDLCMessage _hdlcMessage;
        private IList<IOBISCode> _obisCode;
        
        public Parser(IHDLCMessage message)
        {
            _hdlcMessage = message;
        }

        public Parser()
        {
            _hdlcMessage = new HDLCMessage();
            _obisCode = new List<IOBISCode>();

            var obisCode = new OBISCode()
            {
                ObisCode = "1-0:1.7.0.255",
                ObjectCode = "0100010700FF",
                Unit = "kW",
                Scaler = -3,
                Name = "Active power", 
                Size = 4, 
                DataTypeName = "float"
            };
            
            _obisCode.Add(obisCode) ;
        }

        public IHDLCMessage Parse(List<byte> data)
        {
            _hdlcMessage = new HDLCMessage();

            // var pkt = BitConverter.ToString(data.ToArray()).Replace("-","");

            // Byte 17 is datatype and length (elements)
            // Console(WriteLine($"Parser: {pkt}");
            IHDLCMessage hdlcMessage = new HDLCMessage();

            hdlcMessage.Header.Timestamp = DateTime.Now;
            hdlcMessage.Header.ObjectCount = data[18];
            hdlcMessage.Header.DataType = data[17];
            hdlcMessage.Header.SecondsSinceEpoc = ConvertSecondsToEpoc(DateTime.Now);


            data = data.Skip(18).ToList();

            var messageAsString = string.Concat( data.SelectMany( b => new int[] { b >> 4, b & 0xF }).Select( b => (char)(55 + b + (((b-10)>>31)&-7))) );

            foreach (var obisCode in _obisCode)
            {
                var hdlcData = new HDLCData()
                {
                    Obis_Code = obisCode.ObisCode,
                    Name = obisCode.Name,
                    Unit = obisCode.Unit
                };
                
                if (obisCode.DataTypeName == "float")
                {
                    var value = FindObject<float>(obisCode, messageAsString, data);
                    Console.WriteLine($"Value = {value}");
                    hdlcData.Value = value;
                }
                else
                {
                    var value = FindObject<string>(obisCode, messageAsString, data);
                    Console.WriteLine($"Value = {value}");
                }

                hdlcMessage.Data.Add(hdlcData);
            }

            return hdlcMessage;
        }

        private T FindObject<T>(IOBISCode obisCode, string messageAsString, List<byte> message)
        {
            var pos = messageAsString.IndexOf(obisCode.ObjectCode);
            Console.Write($"Pos: {pos}  ObisCode: {obisCode.ObjectCode}  MessageAsString: {messageAsString}");
            if (pos > 0)
            {
                var startPos = pos + obisCode.ObisCode.Length + 2;
                Console.Write($"  startPos: {startPos}");
                var tmp = Convert.ToHexString(message.Skip(startPos/2).Take(4).ToArray());
                if (typeof(T) == typeof(float))
                {
                    var ret = int.Parse(tmp, System.Globalization.NumberStyles.HexNumber) * Math.Pow(10, obisCode.Scaler);
                    return (T) Convert.ChangeType(ret, typeof(T));
                }

                return (T) Convert.ChangeType(tmp, typeof(T));
            }

            return default(T);
        }
        private int ConvertSecondsToEpoc(DateTime dateTime)
        {
            TimeSpan t = dateTime - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            return secondsSinceEpoch;
        }
        private string ConvertToEpocHexString(DateTime dateTime)
        {
            TimeSpan t = dateTime- new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
              
            byte[] epocByte = BitConverter.GetBytes(secondsSinceEpoch);
            if (BitConverter.IsLittleEndian)
                epocByte = epocByte.Reverse().ToArray();
            var epocString = BitConverter.ToString(epocByte).Replace("-","");

            return epocString;
        }
    }
}