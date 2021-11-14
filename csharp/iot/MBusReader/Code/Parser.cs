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
            hdlcMessage.Header.DataLen = data[18];
            hdlcMessage.Header.DataType = data[17];
            hdlcMessage.Header.SecondsSinceEpoc = ConvertSecondsToEpoc(DateTime.Now);

            if (hdlcMessage.Header.DataLen != 1)
                return hdlcMessage;
            
            var hdlcData = new HDLCData();
            
            hdlcMessage.Data.Add(hdlcData);
    
            hdlcData.Obis_Code += data[OBIS_CODE_START].ToString() + "-" + data[OBIS_CODE_START+1].ToString() + ":"
                                  + data[OBIS_CODE_START+2].ToString() +"." + data[OBIS_CODE_START+3].ToString() + "." 
                                  + data[OBIS_CODE_START+4].ToString() + "." + data[OBIS_CODE_START+5].ToString()  ;
            
            if (hdlcData.Obis_Code == "1-0:1.7.0.255")
            {
                hdlcData.Unit = "kW";
                var tmp = Convert.ToHexString(data.Skip(VALUE_START).Take(4).ToArray());
                if (String.IsNullOrEmpty(tmp))
                    return hdlcMessage;
                
                hdlcData.Value = int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
            }

            return hdlcMessage;
        }

        private int ConvertSecondsToEpoc(DateTime dateTime)
        {
            TimeSpan t = DateTime.Now - new DateTime(1970, 1, 1);
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