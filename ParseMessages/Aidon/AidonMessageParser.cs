using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ParseMessages.Contracts;
using ParseMessages.Entities;

namespace ParseMessages.Aidon
{
    public class AidonMessageParser : IMessageParser
    {
        //const byte[] START_TAG = BitConverter.GetBytes(0x7E);

        const Byte UNKNOWN = 0x00;
        const Byte SEARCHING = 0x01;
        const Byte DATA = 0x02;
        const string START_TAG = "7E";

        const int OBJECTS_2P5SEC = 1;
        const int OBJECTS_10SEC = 12;
        const int OBJECTS_1HOUR = 17;

        const int MIN_FRAME_LEN = 7;

        Byte Status = UNKNOWN;

        public AidonMessageParser()
        {
        }

        public IList<AidonMessageDto> Run(Stream stream)
        {
            IList<AidonMessageDto> result = new List<AidonMessageDto>();
            AidonStreamReader reader = new AidonStreamReader(stream);

            byte[] data = new byte[2];
            byte[] message = null;

            string s;
            int i, packetCount = 0 ;

            StringBuilder package = new StringBuilder();

            int counter = 0;

            Status = SEARCHING;

            //using (BinaryReader reader = new BinaryReader(stream))
            //{
            //    while (stream.Position != stream.Length)
            //    {
            //        if (stream.Position > 0)
            //            reader.ReadByte();

            //        if (stream.Position != stream.Length)
            //            data[1] = reader.ReadByte();
            //        else
            //            break;

            //        while ((!charValid(data[0])) && stream.Position <= stream.Length)
            //        {
            //            data[0] = reader.ReadByte();
            //        }

            //        if (stream.Position != stream.Length)
            //            data[1] = reader.ReadByte();
            //        else
            //            break;

            //        while ((!charValid(data[1])) && stream.Position <= stream.Length)
            //        {
            //            data[1] = reader.ReadByte();
            //        }
            while (!reader.EOF())
            { 
                    data = reader.ReadHexByte();
                    s = Encoding.Default.GetString(data);

                    switch(true)
                    {
                        case bool b when s.Equals(START_TAG) && Status == SEARCHING:
                            Status = DATA;
                            Console.Write($"Package start: ({counter}) ");
                            package = new StringBuilder();

                            break;

                        case bool b when s.Equals(START_TAG) && Status == DATA:
                            Status = SEARCHING;
                            packetCount++;
                            Console.WriteLine($"Package ends(pc:{packetCount} {counter} : pl:{package.Length}): {package.ToString()}");
                            Parse(package); 
                            break;

                        default:
                            package.Append(s);
                            break;
                    }

                    i = Convert.ToInt16(s, 16);

                    //data1 = data1-48;
                    counter += 2;
                }
            

            Console.WriteLine($"Antall data byte(s): {counter}");
            Console.ReadKey();

            return result;
        }

        public IBaseMessage Parse(StringBuilder sb)
        {
            IBaseMessage message = new AidonMessageDto();

            if (sb.Length < MIN_FRAME_LEN)
            {
                return null;
            }

            message.FrameFormat = DecodeFrameFormat(sb);
            message.Length = DecodeMessageLength(sb);
            message.ClientAddress = DecodeClientAddress(sb);
            message.ServerAddress = DecodeServerAddress(sb);
            message.Control = DecodeControl(sb);

            message.HCS = DecodeHCS(sb);
            message.LLC = DecodeLLC(sb);

            string listType = sb.ToString(18 * 2 ,2);

            return message;
        }

        private int DecodeFrameFormat(StringBuilder sb)
        {
            int b = int.Parse(sb[0].ToString(), System.Globalization.NumberStyles.HexNumber);

            return b;
        }

        private int DecodeMessageLength(StringBuilder sb)
        {
            int length = 0;

            string str = sb.ToString(1, 2);
            length = int.Parse(str, System.Globalization.NumberStyles.HexNumber);
            return length;
        }

        private int DecodeClientAddress(StringBuilder sb)
        {
            string str = sb.ToString(4, 2);
            int clientAddress = int.Parse(str, System.Globalization.NumberStyles.HexNumber);

            return clientAddress;
        }

        private int DecodeServerAddress(StringBuilder sb)
        {
            var str = sb.ToString(6,4);
            int serverAddress = int.Parse(str, System.Globalization.NumberStyles.HexNumber);

            return serverAddress;
        }


        private int DecodeControl(StringBuilder sb)
        {
            var str = sb.ToString(10, 2);
            int control = int.Parse(str, System.Globalization.NumberStyles.HexNumber);

            return control;
        }

        private int DecodeHCS(StringBuilder sb)
        {
            var str = sb.ToString(12, 4);
            int hcs = int.Parse(str, System.Globalization.NumberStyles.HexNumber);

            return hcs;
        }

        private string DecodeLLC(StringBuilder sb)
        {
            string llc = sb.ToString(16,6);

            return llc;
        }
        Func<byte, bool> charValid = c => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F');
    }
}
