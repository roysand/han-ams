﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MessageParser.Code;

namespace JustParse
{
    class Progam
    {
        // private static string FileName = @"C:\shared\repo\han-ams\csharp\iot\JustRead\data\binary-2021-11-16-22-59.dat";
        private static string FileName = @"/Users/roy/repo/han-ams/csharp/iot/JustRead/data/binary-2022-01-05-20-22.dat";
        private static byte Control = 0X7E;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world JustParse!!");

            byte b;
            Int16 packageCounter = 0;
            List<byte> bytes = new List<byte>();
            Parser parser = new Parser();
            
            using (FileStream fs = new FileStream(FileName, FileMode.Open))
            {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        while (fs.Position != fs.Length)
                        {
                            b = br.ReadByte();
                            if (b != Control)
                            {
                                b = br.ReadByte();
                            }

                            b = br.ReadByte();
                            
                            bytes.Clear();
                            bytes.Add(b);

                            b = br.ReadByte();
                            while  (b != Control)
                            {
                                bytes.Add(b);
                                b = br.ReadByte();
                            }
                            
                            var result = parser.Parse(bytes);
                            if (result.Data.Count > 2)
                            {
                                Console.Write($"({packageCounter}) - EpocTime: {result.Header.SecondsSinceEpoc} -");
                                
                                foreach (var measurement in result.Data)
                                {
                                    Console.Write($"MeasureType={measurement.Name} Value = {measurement.Value}");
                                }
                            
                                Console.WriteLine();
                                packageCounter++;
                            }
                            
                            
                            if (packageCounter > 1500)
                                return;
                        }
                    }
            }
        }
            
    }
}
