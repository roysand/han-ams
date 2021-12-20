﻿using System;
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

            obisCode = new OBISCode()
            {
                ObisCode = "0-0:96.1.7.255",
                ObjectCode = "0000600107FF",
                Unit = "",
                Scaler = 0,
                Name = "Meter-Type", 
                Size = -1, 
                DataTypeName = "string"            
            };

            _obisCode.Add(obisCode);
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
            hdlcMessage.Header.Hdlc_Length = data.Count();
            hdlcMessage.Header.SecondsSinceEpoc = ConvertSecondsToEpoc(DateTime.Now);


            data = data.Skip(18).ToList();

            var messageAsString = string.Concat( data.SelectMany( b => new int[] { b >> 4, b & 0xF }).Select( b => (char)(55 + b + (((b-10)>>31)&-7))) );

            foreach (var obisCode in _obisCode)
            {
                var hdlcData = new HDLCData()
                {
                    ObisCode = obisCode.ObisCode,
                    Name = obisCode.Name,
                    Unit = obisCode.Unit
                };
                
                if (obisCode.DataTypeName == "float")
                {
                    var value = FindObject<float>(obisCode, messageAsString, data);
                    hdlcData.Value = value;
                }
                else if (obisCode.DataTypeName == "string")
                {
                    var value = FindObject<string>(obisCode, messageAsString, data);
                    hdlcData.Description = value;
                }

                hdlcMessage.Data.Add(hdlcData);
            }

            return hdlcMessage;
        }

        private byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private T FindObject<T>(IOBISCode obisCode, string messageAsString, List<byte> message)
        {
            var pos = messageAsString.IndexOf(obisCode.ObjectCode);
            var dataSize = obisCode.Size;
            
            if (pos > 0)
            {
                if (typeof(T) == typeof(string))
                {
                    var startPos = (pos + obisCode.ObisCode.Length + 2) / 2;
                    var tmpHexValue = Convert.ToHexString(message.Skip((pos + obisCode.ObisCode.Length) / 2).Take(1).ToArray());
                    
                    dataSize = int.Parse(tmpHexValue, System.Globalization.NumberStyles.HexNumber);

                    tmpHexValue = Convert.ToHexString(message.Skip(startPos).Take(dataSize).ToArray());
                    var value = Encoding.UTF8.GetString(StringToByteArray(tmpHexValue));
                    Console.WriteLine($"String value: {value} : {tmpHexValue} ");

                    return (T) Convert.ChangeType(value, typeof(T));
                }
                else if (typeof(T) == typeof(float))
                {
                    var startPos = pos + obisCode.ObisCode.Length + 2;
                    var value = Convert.ToHexString(message.Skip(startPos/2).Take(dataSize).ToArray());
                    var ret = int.Parse(value, System.Globalization.NumberStyles.HexNumber) * Math.Pow(10, obisCode.Scaler);
                    return (T) Convert.ChangeType(ret, typeof(T));
                }
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