using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public Parser(IHDLCMessage message)
        {
            _hdlcMessage = message;
        }

        public Parser()
        {
            _hdlcMessage = new HDLCMessage();
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

            if (hdlcMessage.Header.ObjectCount != 1)
                return hdlcMessage;
            
            var hdlcData = new HDLCData();
            
            hdlcMessage.Data.Add(hdlcData);
    
            hdlcData.Obis_Code += data[OBIS_CODE_START].ToString() + "-" + data[OBIS_CODE_START+1].ToString() + ":"
                                  + data[OBIS_CODE_START+2].ToString() +"." + data[OBIS_CODE_START+3].ToString() + "." 
                                  + data[OBIS_CODE_START+4].ToString() + "." + data[OBIS_CODE_START+5].ToString()  ;
            
            if (hdlcData.Obis_Code == "1-0:1.7.0.255")
            {
                hdlcData.Name = "Effekt 1";
                hdlcData.Unit = "W";
                var tmp = Convert.ToHexString(data.Skip(VALUE_START).Take(4).ToArray());
                if (String.IsNullOrEmpty(tmp))
                    return hdlcMessage;
                
                hdlcData.Value = int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
            }

            data = data.Skip(18).ToList();
            
            for(int i = 0;i <= hdlcMessage.Header.Hdlc_Length; i++)
            {
                var dataType = data[11];
                Console.WriteLine($"DataType = {dataType}");

                if (dataType == TYPE_STRING)
                {
                    var size = Convert.ToHexString(data.Skip(11).Take(1).ToArray());
                    var sizeNum = int.Parse(size, System.Globalization.NumberStyles.HexNumber);
                    var value = data.Skip(12).Take(sizeNum).ToString();
                    data = data.Skip(12 + sizeNum).ToList();
                }
                else if (dataType == TYPE_UINT32)
                {
                    var tmp = Convert.ToHexString(data.Skip(12).Take(4).ToArray());
                    if (!String.IsNullOrEmpty(tmp))
                    {
                        var d = new HDLCData()
                        {
                            Name = "Effekt",
                            Value = int.Parse(tmp, System.Globalization.NumberStyles.HexNumber),
                            Unit = "W"
                        };

                        hdlcMessage.Data.Add(d);
                        data = data.Skip(22).ToList();
                    }
                }
            }
            
            return hdlcMessage;
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