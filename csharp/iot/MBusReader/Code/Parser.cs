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
                Name = "Active power"
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

            // if (hdlcMessage.Header.ObjectCount != 1)
            //    return hdlcMessage;


            data = data.Skip(18).ToList();

            var messageAsString = string.Concat( data.SelectMany( b => new int[] { b >> 4, b & 0xF }).Select( b => (char)(55 + b + (((b-10)>>31)&-7))) );
            for (int i = 0; i < hdlcMessage.Header.ObjectCount; i++)
            {
                // Find all objects in message
                foreach (var obisCode in _obisCode)
                {
                    var value = FindObject(obisCode.ObisCode, messageAsString, data);
                    Console.WriteLine($"Value = {value}");
                }

            }

            return hdlcMessage;
        }

        private string FindObject(string obisCode, string messageAsString, List<byte> message)
        {
            var pos = messageAsString.IndexOf(obisCode);
            Console.Write($"Pos: {pos}  ");
            if (pos > 0)
            {
                var startPos = pos + obisCode.Length;
                var len = int.Parse(messageAsString.Skip(startPos).Take(2).ToString());
                var value = messageAsString.Skip(startPos + 4).Take(len).ToString();

                return value;
            }

            return String.Empty;
        }
        //
        // var strTmp = String.Empty;
        //     strTmp = string.Concat( data.SelectMany( b => new int[] { b >> 4, b & 0xF }).Select( b => (char)(55 + b + (((b-10)>>31)&-7))) );
        //
        //     var hdlcData = new HDLCData();
        //     var pos = strTmp.IndexOf("0100010700FF");
        //     Console.Write($"Pos={pos}, string={strTmp}");
        //     if (pos > 0)
        //     {
        //         var tmp = Convert.ToHexString(data.Skip((2+pos+"0100010700FF".Length)/2).Take(4).ToArray());
        //         if (String.IsNullOrEmpty(tmp))
        //             return hdlcMessage;
        //         // Console.WriteLine($"Value: {int.Parse(tmp, System.Globalization.NumberStyles.HexNumber)} W");
        //         
        //         hdlcData.Name = "ActivePower";
        //         hdlcData.Unit = "W";
        //         
        //         hdlcData.Value = int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
        //         hdlcMessage.Data.Add(hdlcData);
        //     }
        //
        //     hdlcData = new HDLCData();
        //     
        //     hdlcMessage.Data.Add(hdlcData);
        //
        //     hdlcData.Obis_Code += data[OBIS_CODE_START].ToString() + "-" + data[OBIS_CODE_START+1].ToString() + ":"
        //                           + data[OBIS_CODE_START+2].ToString() +"." + data[OBIS_CODE_START+3].ToString() + "." 
        //                           + data[OBIS_CODE_START+4].ToString() + "." + data[OBIS_CODE_START+5].ToString()  ;
        //     
        //     if (hdlcData.Obis_Code == "1-0:1.7.0.255")
        //     {
        //         hdlcData.Name = "ActivePower";
        //         hdlcData.Unit = "W";
        //         var tmp = Convert.ToHexString(data.Skip(VALUE_START).Take(4).ToArray());
        //         if (String.IsNullOrEmpty(tmp))
        //             return hdlcMessage;
        //         
        //         hdlcData.Value = int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
        //     }
        //
        //     data = data.Skip(18).ToList();
        //     
        //     for(int i = 0;i <= hdlcMessage.Header.Hdlc_Length; i++)
        //     {
        //         var dataType = data[11];
        //         Console.WriteLine($"DataType = {dataType}");
        //
        //         if (dataType == TYPE_STRING)
        //         {
        //             var size = Convert.ToHexString(data.Skip(11).Take(1).ToArray());
        //             var sizeNum = int.Parse(size, System.Globalization.NumberStyles.HexNumber);
        //             var value = data.Skip(12).Take(sizeNum).ToString();
        //             data = data.Skip(12 + sizeNum).ToList();
        //         }
        //         else if (dataType == TYPE_UINT32)
        //         {
        //             var tmp = Convert.ToHexString(data.Skip(12).Take(4).ToArray());
        //             if (!String.IsNullOrEmpty(tmp))
        //             {
        //                 var d = new HDLCData()
        //                 {
        //                     Name = "Effekt",
        //                     Value = int.Parse(tmp, System.Globalization.NumberStyles.HexNumber),
        //                     Unit = "W"
        //                 };
        //
        //                 hdlcMessage.Data.Add(d);
        //                 data = data.Skip(22).ToList();
        //             }
        //         }
        //         // else if (dataType == TYPE_INT16)
        //         // {
        //         //     var tmp = Convert.ToHexString(data.Skip(12).Take(2).ToArray());
        //         //     if (!String.IsNullOrEmpty(tmp))
        //         //     {
        //         //         var d = new HDLCData()
        //         //         {
        //         //             Name = "Effekt",
        //         //             Value = int.Parse(tmp, System.Globalization.NumberStyles.HexNumber),
        //         //             Unit = "W"
        //         //         };
        //
        //         //         hdlcMessage.Data.Add(d);
        //         //         data = data.Skip(22).ToList();
        //         //     }
        //         // }
        //     }
        //     
        //     return hdlcMessage;
        // }

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